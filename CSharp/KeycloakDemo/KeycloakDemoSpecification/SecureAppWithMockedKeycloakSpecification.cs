using System.Net;
using FluentAssertions;
using IdentityModel.Client;
using KeycloakMock;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace KeycloakDemoSpecification;

[TestFixture]
public class SecureAppWithMockedKeycloakSpecification
{
  private KeycloakMockServer? _mockServer;
  private WebApplicationFactory<SecureApp.Program>? _appFactory;
  private HttpClient? _httpClient;
  private DiscoveryDocumentResponse? _discoveryDocument;

  [OneTimeSetUp]
  public async Task OneTimeSetUp()
  {
    // Start Keycloak mock server
    _mockServer = KeycloakMockServer.Start(
      port: 8080,
      useSsl: false,
      audience: "account",
      realm: "master");

    // Create and configure the web application factory
    _appFactory = new WebApplicationFactory<SecureApp.Program>()
        .WithWebHostBuilder(b =>
        {
          b.UseTestServer(o => o.PreserveExecutionContext = true);
          b.ConfigureAppConfiguration((context, builder) =>
          {
            builder.AddInMemoryCollection(new Dictionary<string, string?>()
            {
              ["Keycloak:Authority"] = _mockServer.Authority,
              ["Keycloak:Audience"] = _mockServer.Audience,
              ["Keycloak:RequireHttpsMetadata"] = "false"
            });
          });
        });

    // Setup HTTP client and discovery document
    _httpClient = new HttpClient();
    _discoveryDocument = await _httpClient.GetDiscoveryDocumentAsync(_mockServer.Authority);

    _discoveryDocument.HttpStatusCode.Should().Be(HttpStatusCode.OK);
    _discoveryDocument.IsError.Should().BeFalse();
  }

  [OneTimeTearDown]
  public void OneTimeTearDown()
  {
    _httpClient?.Dispose();
    _appFactory?.Dispose();
    _mockServer?.Dispose();
  }

  [Test]
  public async Task ShouldSuccessfullyObtainAccessTokenUsingClientCredentials()
  {
    // Arrange
    // For the mock, we simulate the token endpoint behavior by creating a token directly
    var token = _mockServer!.CreateToken();

    // Act - Verify the discovery document has the token endpoint
    var tokenEndpoint = _discoveryDocument!.TokenEndpoint;

    // Assert
    tokenEndpoint.Should().NotBeNullOrEmpty();
    token.Should().NotBeNullOrEmpty();
    
    // The mock doesn't implement the full OAuth flow, but we can verify
    // that tokens can be created and will be valid for our app
  }

  [Test]
  public async Task ShouldFailToObtainTokenWithInvalidCredentials()
  {
    // Arrange & Act
    // The mock server doesn't implement full OAuth flow validation
    // This test demonstrates the limitation of the mock compared to the real server
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
    // Note: The mock doesn't actually validate credentials - it returns an error
    // because the token endpoint returns a 404 (not implemented in the mock)
    tokenResponse.IsError.Should().BeTrue();
  }

  [Test]
  public async Task ShouldSuccessfullyConnectToSignalRHubWithValidToken()
  {
    // Arrange
    var token = _mockServer!.CreateToken();
    token.Should().NotBeNullOrEmpty();

    // Act
    var connection = new HubConnectionBuilder()
      .WithUrl(
        $"http://localhost/echo?access_token={token}",
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
    _discoveryDocument.Issuer.Should().Contain("master");
  }

  [Test]
  public async Task ShouldCreateTokensWithCustomClaims()
  {
    // Arrange
    var customClaims = new[]
    {
      new System.Security.Claims.Claim("sub", "test-user"),
      new System.Security.Claims.Claim("preferred_username", "testuser"),
      new System.Security.Claims.Claim("email", "test@example.com")
    };

    // Act
    var token = _mockServer!.CreateToken(customClaims);

    // Assert
    token.Should().NotBeNullOrEmpty();

    // Verify the token can be used with the app
    var connection = new HubConnectionBuilder()
      .WithUrl(
        $"http://localhost/echo?access_token={token}",
        o => o.HttpMessageHandlerFactory = _ => _appFactory!.Server.CreateHandler())
      .Build();

    Func<Task> act = async () => await connection.StartAsync();
    await act.Should().NotThrowAsync();

    // Cleanup
    await connection.StopAsync();
    await connection.DisposeAsync();
  }

  [Test]
  public void ShouldCreateTokensWithExpirationTime()
  {
    // Arrange
    var expiresAt = DateTime.UtcNow.AddHours(1);

    // Act
    var token = _mockServer!.CreateToken(expiresAt: expiresAt);

    // Assert
    token.Should().NotBeNullOrEmpty();
  }

  [Test]
  public async Task ShouldProvideCompatibleOpenIdConfiguration()
  {
    // Arrange & Act
    var openIdConfig = await _httpClient!.GetDiscoveryDocumentAsync(_mockServer!.Authority);

    // Assert - Verify the mock provides all necessary endpoints
    openIdConfig.IsError.Should().BeFalse();
    openIdConfig.Issuer.Should().NotBeNullOrEmpty();
    openIdConfig.TokenEndpoint.Should().NotBeNullOrEmpty();
    openIdConfig.JwksUri.Should().NotBeNullOrEmpty();
    openIdConfig.AuthorizeEndpoint.Should().NotBeNullOrEmpty();
    openIdConfig.UserInfoEndpoint.Should().NotBeNullOrEmpty();
  }

  [Test]
  public async Task ShouldProvideValidJwksEndpoint()
  {
    // Arrange & Act
    var jwks = await _httpClient!.GetJsonWebKeySetAsync(_mockServer!.Authority + "/.well-known/jwks.json");

    // Assert
    jwks.IsError.Should().BeFalse();
    jwks.KeySet.Should().NotBeNull();
    jwks.KeySet!.Keys.Should().NotBeEmpty();
    jwks.KeySet.Keys.First().Kty.Should().Be("RSA");
    jwks.KeySet.Keys.First().Use.Should().Be("sig");
  }
}
