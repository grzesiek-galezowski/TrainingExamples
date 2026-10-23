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
    await new TrackClinicKeycloakBootstrap(_client).Execute(
      KeycloakConstants.RealmName, 
      KeycloakConstants.ClientId, 
      KeycloakConstants.ArtistRole, 
      KeycloakConstants.AdminRole);
  }

  public static async Task Stop()
  {
    await KeycloakContainer.StopAsync();
  }

  public static string GetAuthority() =>
    $"{KeycloakContainer.GetBaseAddress()}realms/{KeycloakConstants.RealmName}";

  public static string GetMetadataAddress() =>
    $"{GetAuthority()}/.well-known/openid-configuration";

  public static async Task<string> GetToken(string username, string password)
  {
    return await _client.OrThrow().GetToken(KeycloakConstants.RealmName, KeycloakConstants.ClientId, username, password);
  }
}