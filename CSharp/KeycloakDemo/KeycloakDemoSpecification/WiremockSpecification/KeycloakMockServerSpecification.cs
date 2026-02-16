using AwesomeAssertions;
using IdentityModel.Client;
using KeycloakMock;
using NUnit.Framework;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;

namespace KeycloakDemoSpecification.WiremockSpecification;

[TestFixture]
public class KeycloakMockServerSpecification
{
  [Test]
  public void ShouldStartMockServerSuccessfully()
  {
    // GIVEN - WHEN
    using var mockServer = KeycloakMockServer.Start(
      port: 8080,
      useSsl: false,
      audience: "test-audience",
      realm: "test-realm");

    // THEN
    mockServer.Should().NotBeNull();
    mockServer.TokenGenerator.Should().NotBeNull();
    mockServer.TokenGenerator.Authority.Should().Contain("test-realm");
    mockServer.TokenGenerator.Audience.Should().Be("test-audience");
  }

  [Test]
  public async Task ShouldProvideValidOpenIdConfiguration()
  {
    // GIVEN
    using var mockServer = KeycloakMockServer.Start(
      port: 8081,
      useSsl: false,
      audience: "test-audience",
      realm: "test-realm");
    mockServer.ConfigureJwksEndpoint();
    using var httpClient = new HttpClient();

    // WHEN
    var discoveryDocument = await httpClient.GetDiscoveryDocumentAsync(
      mockServer.TokenGenerator.Authority);

    // THEN
    discoveryDocument.Should().NotBeNull();
    discoveryDocument.IsError.Should().BeFalse();
    discoveryDocument.HttpStatusCode.Should().Be(HttpStatusCode.OK);
    discoveryDocument.Issuer.Should().NotBeNullOrEmpty();
    discoveryDocument.TokenEndpoint.Should().NotBeNullOrEmpty();
    discoveryDocument.JwksUri.Should().NotBeNullOrEmpty();
    discoveryDocument.AuthorizeEndpoint.Should().NotBeNullOrEmpty();
    discoveryDocument.UserInfoEndpoint.Should().NotBeNullOrEmpty();
  }

  [Test]
  public async Task ShouldProvideValidJwksEndpoint()
  {
    // GIVEN
    using var mockServer = KeycloakMockServer.Start(
      port: 8082,
      useSsl: false,
      audience: "test-audience",
      realm: "test-realm");

    mockServer.ConfigureJwksEndpoint();
    using var httpClient = new HttpClient();

    // WHEN
    var jwks = await httpClient.GetJsonWebKeySetAsync(
      mockServer.TokenGenerator.Authority + "/.well-known/jwks.json");

    // THEN
    jwks.Should().NotBeNull();
    jwks.IsError.Should().BeFalse();
    jwks.KeySet.Should().NotBeNull();
    jwks.KeySet!.Keys.Should().NotBeEmpty();
    jwks.KeySet.Keys.First().Kty.Should().Be("RSA");
    jwks.KeySet.Keys.First().Use.Should().Be("sig");
  }

  [Test]
  public void ShouldCreateTokenWithDefaultClaims()
  {
    // GIVEN
    using var mockServer = KeycloakMockServer.Start(
      port: 8083,
      useSsl: false,
      audience: "test-audience",
      realm: "test-realm");

    // WHEN
    var token = mockServer.CreateToken();

    // THEN
    token.Should().NotBeNullOrEmpty();

    var handler = new JwtSecurityTokenHandler();
    var jwtToken = handler.ReadJwtToken(token);

    jwtToken.Issuer.Should().Be(mockServer.TokenGenerator.Authority + "/");
    jwtToken.Audiences.Should().Contain("test-audience");
  }

  [Test]
  public void ShouldCreateTokenWithCustomClaims()
  {
    // GIVEN
    using var mockServer = KeycloakMockServer.Start(
      port: 8084,
      useSsl: false,
      audience: "test-audience",
      realm: "test-realm");

    var customClaims = new[]
    {
      new Claim("sub", "test-user"),
      new Claim("preferred_username", "testuser"),
      new Claim("email", "test@example.com"),
      new Claim("custom_claim", "custom_value")
    };

    // WHEN
    var token = mockServer.CreateToken(customClaims);

    // THEN
    token.Should().NotBeNullOrEmpty();

    var handler = new JwtSecurityTokenHandler();
    var jwtToken = handler.ReadJwtToken(token);

    jwtToken.Claims.Should().Contain(c => c.Type == "sub" && c.Value == "test-user");
    jwtToken.Claims.Should().Contain(c => c.Type == "preferred_username" && c.Value == "testuser");
    jwtToken.Claims.Should().Contain(c => c.Type == "email" && c.Value == "test@example.com");
    jwtToken.Claims.Should().Contain(c => c.Type == "custom_claim" && c.Value == "custom_value");
  }

  [Test]
  public void ShouldCreateTokenWithCustomExpiration()
  {
    // GIVEN
    using var mockServer = KeycloakMockServer.Start(
      port: 8085,
      useSsl: false,
      audience: "test-audience",
      realm: "test-realm");

    var expiresAt = DateTime.UtcNow.AddHours(2);

    // WHEN
    var token = mockServer.CreateToken(expiresAt: expiresAt);

    // THEN
    token.Should().NotBeNullOrEmpty();

    var handler = new JwtSecurityTokenHandler();
    var jwtToken = handler.ReadJwtToken(token);

    jwtToken.ValidTo.Should().BeCloseTo(expiresAt, TimeSpan.FromSeconds(1));
  }

  [Test]
  public void ShouldCreateExpiredToken()
  {
    // GIVEN
    using var mockServer = KeycloakMockServer.Start(
      port: 8086,
      useSsl: false,
      audience: "test-audience",
      realm: "test-realm");

    var expiresAt = DateTime.UtcNow.AddHours(-1); // expired 1 hour ago
    mockServer.ConfigureJwksEndpoint();

    // WHEN
    var token = mockServer.CreateToken(expiresAt: expiresAt);

    // THEN
    token.Should().NotBeNullOrEmpty();

    var handler = new JwtSecurityTokenHandler();
    var jwtToken = handler.ReadJwtToken(token);

    jwtToken.ValidTo.Should().BeBefore(DateTime.UtcNow);
  }

  [Test]
  public void ShouldCreateMultipleTokens()
  {
    // GIVEN
    using var mockServer = KeycloakMockServer.Start(
      port: 8087,
      useSsl: false,
      audience: "test-audience",
      realm: "test-realm");

    // WHEN - Create tokens with different claims to ensure uniqueness
    var token1 = mockServer.CreateToken([new Claim("sub", "user-1")]);
    var token2 = mockServer.CreateToken([new Claim("sub", "user-2")]);

    // THEN
    token1.Should().NotBeNullOrEmpty();
    token2.Should().NotBeNullOrEmpty();
    token1.Should().NotBe(token2); // Tokens should be different due to different claims
  }

  [Test]
  public async Task ShouldSupportMultipleRealms()
  {
    // GIVEN
    using var mockServer1 = KeycloakMockServer.Start(
      port: 8088,
      useSsl: false,
      audience: "audience1",
      realm: "realm1");

    using var mockServer2 = KeycloakMockServer.Start(
      port: 8089,
      useSsl: false,
      audience: "audience2",
      realm: "realm2");

    mockServer1.ConfigureJwksEndpoint();
    mockServer2.ConfigureJwksEndpoint();
    using var httpClient = new HttpClient();

    // WHEN
    var discovery1 = await httpClient.GetDiscoveryDocumentAsync(mockServer1.TokenGenerator.Authority);
    var discovery2 = await httpClient.GetDiscoveryDocumentAsync(mockServer2.TokenGenerator.Authority);

    // THEN
    discovery1.Issuer.Should().Contain("realm1");
    discovery2.Issuer.Should().Contain("realm2");

    var token1 = mockServer1.CreateToken();
    var token2 = mockServer2.CreateToken();

    var handler = new JwtSecurityTokenHandler();
    var jwtToken1 = handler.ReadJwtToken(token1);
    var jwtToken2 = handler.ReadJwtToken(token2);

    jwtToken1.Audiences.Should().Contain("audience1");
    jwtToken2.Audiences.Should().Contain("audience2");
  }

  [Test]
  public void ShouldDisposeCleanly()
  {
    // GIVEN
    var mockServer = KeycloakMockServer.Start(
      port: 8090,
      useSsl: false,
      audience: "test-audience",
      realm: "test-realm");

    var token = mockServer.CreateToken();
    token.Should().NotBeNullOrEmpty();

    // WHEN - THEN
    var disposeAction = mockServer.Dispose;
    disposeAction.Should().NotThrow();
  }
}
