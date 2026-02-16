using System.Globalization;
using AwesomeAssertions;
using KeycloakMock;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using SecureApp;
using System.Security.Claims;
using IdentityModel.Client;

namespace KeycloakDemoSpecification.WiremockSpecification;

public class SecureAppDriver : IAsyncDisposable
{
  private readonly KeycloakMockServer? _keycloakMockServer;
  private readonly WebApplicationFactory<Program>? _appFactory;
  private HttpClient? _httpClient;
  private DiscoveryDocumentResponse? _discoveryDocument;
  private HubConnection _connection;

  private SecureAppDriver(KeycloakMockServer keycloakMockServer, WebApplicationFactory<Program> hostBuilder)
  {
    _keycloakMockServer = keycloakMockServer;
    _appFactory = hostBuilder;

  }

  public static SecureAppDriver Create()
  {
    var keycloakMockServer = KeycloakMockServer.Start(
      port: 8080,
      useSsl: false,
      audience: "account",
      realm: "master");
    return new SecureAppDriver(
      keycloakMockServer,
      new WebApplicationFactory<Program>()
        .WithWebHostBuilder(b =>
        {
          b.UseTestServer(o => o.PreserveExecutionContext = true);
          b.ConfigureAppConfiguration((_, builder) =>
          {
            builder.AddInMemoryCollection(new Dictionary<string, string?>
            {
              ["Keycloak:Authority"] = keycloakMockServer.TokenGenerator.Authority,
              ["Keycloak:Audience"] = keycloakMockServer.TokenGenerator.Audience,
              ["Keycloak:RequireHttpsMetadata"] = false.ToString(CultureInfo.InvariantCulture)
            });
          });
        }));
  }

  public async Task Start()
  {
    _keycloakMockServer.ConfigureJwksEndpoint();
    _keycloakMockServer.ConfigureTokenEndpoint();
    _httpClient = new HttpClient();
    _discoveryDocument = await _httpClient.GetDiscoveryDocumentAsync(
      _keycloakMockServer.TokenGenerator.Authority);
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

  public DiscoveryDocumentResponse GetDiscoveryDocument()
  {
    return _discoveryDocument!;
  }

  public async Task ConnectToHub(string token)
  {
    _connection = new HubConnectionBuilder()
      .WithUrl(
        "http://localhost/echo",
        o =>
        {
          o.HttpMessageHandlerFactory = _ => _appFactory!.Server.CreateHandler();
          o.AccessTokenProvider = async () => token;
          o.WebSocketFactory = async (context, cancellationToken) =>
          {
            var wsClient = _appFactory!.Server.CreateWebSocketClient();
            var uri = new UriBuilder(context.Uri);
            return await wsClient.ConnectAsync(uri.Uri, cancellationToken);
          };
        })
      .Build();

    await _connection.StartAsync();
    _connection.State.Should().Be(HubConnectionState.Connected);
  }

  public async ValueTask DisposeAsync()
  {
    if (_keycloakMockServer != null) await CastAndDispose(_keycloakMockServer);
    if (_appFactory != null) await _appFactory.DisposeAsync();
    if (_httpClient != null) await CastAndDispose(_httpClient);
    await (_connection?.DisposeAsync() ?? ValueTask.CompletedTask);

    return;

    static async ValueTask CastAndDispose(IDisposable resource)
    {
      if (resource is IAsyncDisposable resourceAsyncDisposable)
        await resourceAsyncDisposable.DisposeAsync();
      else
        resource.Dispose();
    }
  }

  public Task ConnectToHubWithCustomClaims(Claim[] customClaims)
  {
    return ConnectToHub(_keycloakMockServer!.CreateToken(customClaims));
  }

  public async Task ConnectToHub()
  {
    var tokenResponse = await ObtainAccessToken();
    tokenResponse.AccessToken.Should().NotBeNullOrEmpty();
    await ConnectToHub(tokenResponse.AccessToken!);
  }
}