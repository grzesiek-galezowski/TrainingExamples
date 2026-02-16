using System.Security.Claims;
using System.Net;
using AwesomeAssertions;
using NUnit.Framework;

namespace KeycloakDemoSpecification.WiremockSpecification;

[TestFixture]
public class SecureAppWithMockedKeycloakSpecification
{
  [Test]
  public async Task ShouldSuccessfullyObtainAccessTokenUsingClientCredentials()
  {
    // GIVEN
    await using var driver = SecureAppDriver.Create();
    await driver.Start();

    // WHEN
    var tokenResponse = await driver.ObtainAccessToken();

    // THEN
    tokenResponse.HttpStatusCode.Should().Be(HttpStatusCode.OK);
    tokenResponse.IsError.Should().BeFalse();
    tokenResponse.AccessToken.Should().NotBeNullOrEmpty();
    tokenResponse.TokenType.Should().Be("Bearer");
  }

  [Test]
  public async Task ShouldSuccessfullyConnectToSignalRHubWithValidToken()
  {
    // GIVEN
    await using var driver = SecureAppDriver.Create();
    await driver.Start();

    // WHEN - THEN
    await new Func<Task>(async () => await driver.ConnectToHub())
      .Should().NotThrowAsync();

  }

  [Test]
  public async Task ShouldFailToConnectToSignalRHubWithoutToken()
  {
    // GIVEN
    await using var driver = SecureAppDriver.Create();
    await driver.Start();

    // WHEN - THEN
    await new Func<Task>(async () => await driver.ConnectToHub("")).Should().ThrowAsync<HttpRequestException>();
  }

  [Test]
  public async Task ShouldFailToConnectToSignalRHubWithInvalidToken()
  {
    // GIVEN
    await using var driver = SecureAppDriver.Create();
    await driver.Start();

    // WHEN - THEN
    await new Func<Task>(async () => await driver.ConnectToHub("invalid-token")).Should().ThrowAsync<HttpRequestException>();
  }

  [Test]
  public async Task ShouldRetrieveDiscoveryDocumentSuccessfully()
  {
    // GIVEN
    await using var driver = SecureAppDriver.Create();
    await driver.Start();

    // WHEN
    var discoveryDocument = driver.GetDiscoveryDocument();

    // THEN
    discoveryDocument.Should().NotBeNull();
    discoveryDocument.TokenEndpoint.Should().NotBeNullOrEmpty();
    discoveryDocument.JwksUri.Should().NotBeNullOrEmpty();
    discoveryDocument.Issuer.Should().Contain("/master");
  }

  [Test]
  public async Task ShouldCreateTokensWithCustomClaims()
  {
    // GIVEN
    await using var driver = SecureAppDriver.Create();
    await driver.Start();

    var customClaims = new[]
    {
      new Claim("sub", "test-user"),
      new Claim("preferred_username", "testuser"),
      new Claim("email", "test@example.com")
    };

    //WHEN - THEN
    await new Func<Task>(async () =>
    {
      await driver.ConnectToHubWithCustomClaims(customClaims);
    }).Should().NotThrowAsync();
  }
}
