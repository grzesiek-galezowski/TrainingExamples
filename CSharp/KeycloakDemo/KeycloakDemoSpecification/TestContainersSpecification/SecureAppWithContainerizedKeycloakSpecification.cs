using System.Net;
using AwesomeAssertions;
using NUnit.Framework;

namespace KeycloakDemoSpecification.TestContainersSpecification;

[TestFixture]
public class SecureAppWithContainerizedKeycloakSpecification
{
  [Test]
  public async Task ShouldSuccessfullyObtainAccessTokenUsingClientCredentials()
  {
    // GIVEN
    await using var driver = await SecureAppDriver.Create();
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
  public async Task ShouldFailToObtainTokenWithInvalidCredentials()
  {
    // GIVEN
    await using var driver = await SecureAppDriver.Create();
    await driver.Start();

    // WHEN
    var tokenResponse = await driver.ObtainAccessTokenWithInvalidCredentials();

    // THEN
    tokenResponse.IsError.Should().BeTrue();
    tokenResponse.Error.Should().Be("Unauthorized");
  }

  [Test]
  public async Task ShouldSuccessfullyConnectToSignalRHubWithValidToken()
  {
    // GIVEN
    await using var driver = await SecureAppDriver.Create();
    await driver.Start();

    // WHEN - THEN
    await new Func<Task>(async () => await driver.ConnectToHub())
      .Should().NotThrowAsync();
  }

  [Test]
  public async Task ShouldFailToConnectToSignalRHubWithoutToken()
  {
    // GIVEN
    await using var driver = await SecureAppDriver.Create();
    await driver.Start();

    // WHEN - THEN
    await new Func<Task>(async () => await driver.ConnectToHub(""))
      .Should().ThrowAsync<HttpRequestException>();
  }

  [Test]
  public async Task ShouldFailToConnectToSignalRHubWithInvalidToken()
  {
    // GIVEN
    await using var driver = await SecureAppDriver.Create();
    await driver.Start();

    // WHEN - THEN
    await new Func<Task>(async () => await driver.ConnectToHub("invalid-token"))
      .Should().ThrowAsync<HttpRequestException>();
  }

  [Test]
  public async Task ShouldRetrieveDiscoveryDocumentSuccessfully()
  {
    // GIVEN
    await using var driver = await SecureAppDriver.Create();
    await driver.Start();

    // WHEN
    var discoveryDocument = driver.GetDiscoveryDocument();

    // THEN
    discoveryDocument.Should().NotBeNull();
    discoveryDocument.TokenEndpoint.Should().NotBeNullOrEmpty();
    discoveryDocument.JwksUri.Should().NotBeNullOrEmpty();
    discoveryDocument.Issuer.Should().Contain("realms/master");
  }
}
