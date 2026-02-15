using System.Net;
using FluentAssertions;
using IdentityModel.Client;
using Keycloak.Net.Models.Clients;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Testcontainers.Keycloak;

namespace KeycloakDemoSpecification;

[TestFixture]
public class SecureAppWithContainerizedKeycloakSpecification
{
  private KeycloakContainer? _container;
  private WebApplicationFactory<SecureApp.Program>? _appFactory;
  private HttpClient? _httpClient;
  private Keycloak.Net.KeycloakClient? _keycloakClient;
  private DiscoveryDocumentResponse? _discoveryDocument;

  [OneTimeSetUp]
  public async Task OneTimeSetUp()
  {
    // Start Keycloak container
    _container = new KeycloakBuilder().Build();
    await _container.StartAsync();

    // Create and configure the web application factory
    _appFactory = new WebApplicationFactory<SecureApp.Program>()
        .WithWebHostBuilder(b =>
        {
          b.UseTestServer(o => o.PreserveExecutionContext = true);
          b.ConfigureAppConfiguration((context, builder) =>
          {
            builder.AddInMemoryCollection(new Dictionary<string, string?>()
            {
              ["Keycloak:Authority"] = _container.GetBaseAddress() + "realms/master",
              ["Keycloak:Audience"] = "account",
              ["Keycloak:RequireHttpsMetadata"] = "false"
            });
          });
        });

    // Create Keycloak admin client
    _keycloakClient = new Keycloak.Net.KeycloakClient(
      _container.GetBaseAddress(),
      "admin",
      "admin");

    // Create a client for testing
    var clientCreated = await _keycloakClient.CreateClientAsync("master", new Client()
    {
      ClientId = "test-client",
      Access = new ClientAccess()
      {
        Configure = true,
        Manage = true,
        View = true
      },
      Enabled = true,
      Secret = "test-secret",
      FullScopeAllowed = true,
      DefaultClientScopes = new List<string>()
      {
        "openid", "acr",
        "address",
        "email",
        "profile",
        "roles",
        "web-origins",
      },
      OptionalClientScopes = new List<string>()
      {
        "microprofile-jwt Optional",
        "offline_access Optional",
        "phone Optional",
      },
      ServiceAccountsEnabled = true,
      AuthorizationServicesEnabled = true,
    });

    clientCreated.Should().BeTrue();

    // Setup HTTP client and discovery document
    _httpClient = new HttpClient();
    _discoveryDocument = await _httpClient.GetDiscoveryDocumentAsync(
      _container.GetBaseAddress() + "realms/master");

    _discoveryDocument.HttpStatusCode.Should().Be(HttpStatusCode.OK);
    _discoveryDocument.IsError.Should().BeFalse();
  }

  [OneTimeTearDown]
  public async Task OneTimeTearDown()
  {
    _httpClient?.Dispose();
    _appFactory?.Dispose();
    
    if (_container != null)
    {
      await _container.StopAsync();
      await _container.DisposeAsync();
    }
  }

  [Test]
  public async Task ShouldSuccessfullyObtainAccessTokenUsingClientCredentials()
  {
    // Arrange & Act
    var tokenResponse = await _httpClient!.RequestClientCredentialsTokenAsync(
      new ClientCredentialsTokenRequest()
      {
        Address = _discoveryDocument!.TokenEndpoint,
        ClientId = "test-client",
        ClientSecret = "test-secret",
        Scope = "openid",
        Parameters = new Parameters()
      });

    // Assert
    tokenResponse.HttpStatusCode.Should().Be(HttpStatusCode.OK);
    tokenResponse.IsError.Should().BeFalse();
    tokenResponse.AccessToken.Should().NotBeNullOrEmpty();
    tokenResponse.TokenType.Should().Be("Bearer");
  }

  [Test]
  public async Task ShouldFailToObtainTokenWithInvalidCredentials()
  {
    // Arrange & Act
    var tokenResponse = await _httpClient!.RequestClientCredentialsTokenAsync(
      new ClientCredentialsTokenRequest()
      {
        Address = _discoveryDocument!.TokenEndpoint,
        ClientId = "test-client",
        ClientSecret = "wrong-secret",
        Scope = "openid",
        Parameters = new Parameters()
      });

    // Assert
    tokenResponse.IsError.Should().BeTrue();
    tokenResponse.Error.Should().Be("Unauthorized");
  }

  [Test]
  public async Task ShouldSuccessfullyConnectToSignalRHubWithValidToken()
  {
    // Arrange
    var tokenResponse = await _httpClient!.RequestClientCredentialsTokenAsync(
      new ClientCredentialsTokenRequest()
      {
        Address = _discoveryDocument!.TokenEndpoint,
        ClientId = "test-client",
        ClientSecret = "test-secret",
        Scope = "openid",
        Parameters = new Parameters()
      });

    tokenResponse.AccessToken.Should().NotBeNullOrEmpty();

    // Act
    var connection = new HubConnectionBuilder()
      .WithUrl(
        $"http://localhost/echo?access_token={tokenResponse.AccessToken}",
        o => o.HttpMessageHandlerFactory = _ => _appFactory!.Server.CreateHandler())
      .Build();

    Func<Task> act = async () => await connection.StartAsync();

    // Assert
    await act.Should().NotThrowAsync();
    connection.State.Should().Be(HubConnectionState.Connected);

    // Cleanup
    await connection.StopAsync();
    await connection.DisposeAsync();
  }

  [Test]
  public async Task ShouldFailToConnectToSignalRHubWithoutToken()
  {
    // Arrange & Act
    var connection = new HubConnectionBuilder()
      .WithUrl(
        "http://localhost/echo",
        o => o.HttpMessageHandlerFactory = _ => _appFactory!.Server.CreateHandler())
      .Build();

    Func<Task> act = async () => await connection.StartAsync();

    // Assert - Should fail because RoleRestrictedRequirement requires authentication
    await act.Should().ThrowAsync<HttpRequestException>();

    // Cleanup
    await connection.DisposeAsync();
  }

  [Test]
  public async Task ShouldFailToConnectToSignalRHubWithInvalidToken()
  {
    // Arrange & Act
    var connection = new HubConnectionBuilder()
      .WithUrl(
        "http://localhost/echo?access_token=invalid-token",
        o => o.HttpMessageHandlerFactory = _ => _appFactory!.Server.CreateHandler())
      .Build();

    Func<Task> act = async () => await connection.StartAsync();

    // Assert
    // Invalid token fails JWT validation and user won't be authenticated
    // This causes the RoleRestrictedRequirement to fail
    await act.Should().ThrowAsync<HttpRequestException>();

    // Cleanup
    await connection.DisposeAsync();
  }

  [Test]
  public async Task ShouldRetrieveDiscoveryDocumentSuccessfully()
  {
    // Assert
    _discoveryDocument.Should().NotBeNull();
    _discoveryDocument!.TokenEndpoint.Should().NotBeNullOrEmpty();
    _discoveryDocument.JwksUri.Should().NotBeNullOrEmpty();
    _discoveryDocument.Issuer.Should().Contain("realms/master");
  }
}
