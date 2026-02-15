using FluentAssertions;

namespace KeycloakDemoSpecification;

[TestFixture]
public class SecureAppWithMockedKeycloakSpecification
{

  [Test]
  public async Task ShouldSuccessfullyConnectToSignalRHubWithValidToken()
  {
    await using var driver = SecureAppDriver.Create();
    driver.Start();

    await new Func<Task>(async () => await driver.ConnectToHub(driver._mockServer!.CreateToken()))
      .Should().NotThrowAsync();

  }

  [Test]
  public async Task ShouldFailToConnectToSignalRHubWithoutToken()
  {
    // Arrange & Act
    await using var driver = SecureAppDriver.Create();
    driver.Start();

    // Assert - Should fail because RoleRestrictedRequirement requires authentication
    await new Func<Task>(async () => await driver.ConnectToHub("")).Should().ThrowAsync<HttpRequestException>();
  }

  [Test]
  public async Task ShouldFailToConnectToSignalRHubWithInvalidToken()
  {
    // Arrange & Act
    await using var driver = SecureAppDriver.Create();
    driver.Start();

    await new Func<Task>(async () => await driver.ConnectToHub("invalid-token")).Should().ThrowAsync<HttpRequestException>();
  }

  [Test]
  public async Task ShouldCreateTokensWithCustomClaims()
  {
    // Arrange
    await using var driver = SecureAppDriver.Create();
    driver.Start();

    var customClaims = new[]
    {
      new System.Security.Claims.Claim("sub", "test-user"),
      new System.Security.Claims.Claim("preferred_username", "testuser"),
      new System.Security.Claims.Claim("email", "test@example.com")
    };

    await new Func<Task>(async () =>
    {
      await driver.ConnectToHub(driver._mockServer!.CreateToken(customClaims));
    }).Should().NotThrowAsync();
  }
}
