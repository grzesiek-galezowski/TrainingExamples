using Core.NullableReferenceTypesExtensions;
using Testcontainers.Keycloak;
using TrackClinic;

namespace TrackClinicServiceSpecification.Automation.Keycloak;

public static class SharedKeycloakInstance
{
  private static readonly KeycloakContainer KeycloakContainer = new KeycloakBuilder("quay.io/keycloak/keycloak:26.0")
    .Build();

  private static IKeycloakClient? _client;

  public static async Task Start()
  {
    await KeycloakContainer.StartAsync();
    _client = new KeycloakClient(KeycloakContainer.GetBaseAddress());
  }

  public static async Task ConfigureNewRealm(TrackClinicKeycloakBootstrap bootstrap)
  {
    await bootstrap.Execute(
      _client.OrThrow(),
      KeycloakConstants.ClientId,
      KeycloakConstants.ArtistRole,
      KeycloakConstants.AdminRole);
  }

  public static async Task Stop()
  {
    await KeycloakContainer.StopAsync();
  }

  public static string GetAuthority(string realmName) =>
    $"{KeycloakContainer.GetBaseAddress()}realms/{realmName}";

  public static string GetMetadataAddress(string realmName) =>
    $"{GetAuthority(realmName)}/.well-known/openid-configuration";

  public static async Task<string> GetToken(string realmName, string username, string password)
  {
    return await _client.OrThrow().GetToken(realmName, KeycloakConstants.ClientId, username, password);
  }

  public static async Task<TestAuthTokens> GetTestAuthTokens(string realmName)
  {
    var adminToken = await SharedKeycloakInstance.GetToken(
      realmName,
      KeycloakConstants.AdminUser,
      "password");
    var unprivilegedToken = await SharedKeycloakInstance.GetToken(
      realmName,
      KeycloakConstants.UnprivilegedUser,
      "password");
    var artistToken = await SharedKeycloakInstance.GetToken(
      realmName,
      KeycloakConstants.ArtistUser,
      "password");
    var testAuthTokens = new TestAuthTokens(adminToken, unprivilegedToken, artistToken);
    return testAuthTokens;
  }
}