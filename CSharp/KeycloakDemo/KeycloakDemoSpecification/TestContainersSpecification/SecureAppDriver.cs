using System.Net;
using System.Security.Claims;
using AwesomeAssertions;
using IdentityModel.Client;
using Keycloak.Net.Models.Clients;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using SecureApp;
using Testcontainers.Keycloak;

namespace KeycloakDemoSpecification.TestContainersSpecification;

public class SecureAppDriver : IAsyncDisposable
{
  private readonly KeycloakContainer _container;
  private readonly WebApplicationFactory<Program> _appFactory;
  private readonly Keycloak.Net.KeycloakClient _keycloakClient;
  private HttpClient? _httpClient;
  private DiscoveryDocumentResponse? _discoveryDocument;
  private HubConnection? _connection;

  private SecureAppDriver(
    KeycloakContainer container,
    WebApplicationFactory<Program> appFactory,
    Keycloak.Net.KeycloakClient keycloakClient)
  {
    _container = container;
    _appFactory = appFactory;
    _keycloakClient = keycloakClient;
  }

  public static async Task<SecureAppDriver> Create()
  {
    // GIVEN a Keycloak container
    var container = new KeycloakBuilder().Build();
    await container.StartAsync();

    // GIVEN Keycloak admin client
    var keycloakClient = new Keycloak.Net.KeycloakClient(
      container.GetBaseAddress(),
      "admin",
      "admin");

    // GIVEN a configured test client in Keycloak
    var clientCreated = await keycloakClient.CreateClientAsync("master", new Client
    {
      ClientId = "test-client",
      Access = new ClientAccess
      {
        Configure = true,
        Manage = true,
        View = true
      },
      Enabled = true,
      Secret = "test-secret",
      FullScopeAllowed = true,
      DefaultClientScopes = new List<string>
      {
        "openid", "acr",
        "address",
        "email",
        "profile",
        "roles",
        "web-origins",
      },
      OptionalClientScopes = new List<string>
      {
        "microprofile-jwt Optional",
        "offline_access Optional",
        "phone Optional",
      },
      ServiceAccountsEnabled = true,
      AuthorizationServicesEnabled = true,
    });

    clientCreated.Should().BeTrue();

    // GIVEN the web application factory configured to use the containerized Keycloak
    var appFactory = new WebApplicationFactory<Program>()
      .WithWebHostBuilder(b =>
      {
        b.UseTestServer(o => o.PreserveExecutionContext = true);
        b.ConfigureAppConfiguration((context, builder) =>
        {
          builder.AddInMemoryCollection(new Dictionary<string, string?>
          {
            ["Keycloak:Authority"] = container.GetBaseAddress() + "realms/master",
            ["Keycloak:Audience"] = "account",
            ["Keycloak:RequireHttpsMetadata"] = "false"
          });
        });
      });

    return new SecureAppDriver(container, appFactory, keycloakClient);
  }

  public async Task Start()
  {
    // Setup HTTP client and discovery document
    _httpClient = new HttpClient();
    _discoveryDocument = await _httpClient.GetDiscoveryDocumentAsync(
      _container.GetBaseAddress() + "realms/master");

    _discoveryDocument.HttpStatusCode.Should().Be(HttpStatusCode.OK);
    _discoveryDocument.IsError.Should().BeFalse();
  }

  public async Task<TokenResponse> ObtainAccessToken()
  {
    var tokenResponse = await _httpClient!.RequestClientCredentialsTokenAsync(
      new ClientCredentialsTokenRequest
      {
        Address = _discoveryDocument!.TokenEndpoint,
        ClientId = "test-client",
        ClientSecret = "test-secret",
        Scope = "openid",
        Parameters = []
      });

    return tokenResponse;
  }

  public async Task<TokenResponse> ObtainAccessTokenWithInvalidCredentials()
  {
    var tokenResponse = await _httpClient!.RequestClientCredentialsTokenAsync(
      new ClientCredentialsTokenRequest
      {
        Address = _discoveryDocument!.TokenEndpoint,
        ClientId = "test-client",
        ClientSecret = "wrong-secret",
        Scope = "openid",
        Parameters = []
      });

    return tokenResponse;
  }

  public async Task ConnectToHub(string token)
  {
    _connection = new HubConnectionBuilder()
      .WithUrl(
        "http://localhost/echo",
        o =>
        {
          o.HttpMessageHandlerFactory = _ => _appFactory.Server.CreateHandler();
          o.AccessTokenProvider = async () => token;
          o.WebSocketFactory = async (context, cancellationToken) =>
          {
            var wsClient = _appFactory.Server.CreateWebSocketClient();
            var uri = new UriBuilder(context.Uri);
            return await wsClient.ConnectAsync(uri.Uri, cancellationToken);
          };
        })
      .Build();

    await _connection.StartAsync();
    _connection.State.Should().Be(HubConnectionState.Connected);
  }

  public async Task ConnectToHub()
  {
    var tokenResponse = await ObtainAccessToken();
    tokenResponse.AccessToken.Should().NotBeNullOrEmpty();
    await ConnectToHub(tokenResponse.AccessToken!);
  }

  public DiscoveryDocumentResponse GetDiscoveryDocument()
  {
    return _discoveryDocument!;
  }

  public async ValueTask DisposeAsync()
  {
    if (_connection != null)
    {
      await _connection.StopAsync();
      await _connection.DisposeAsync();
    }

    _httpClient?.Dispose();
    await _appFactory.DisposeAsync();

    await _container.StopAsync();
    await _container.DisposeAsync();
  }
}
