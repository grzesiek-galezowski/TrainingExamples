using ServiceBuildingBlocks;

namespace ServiceSpecificationBuildingBlocks.Keycloak;

public class TrackClinicKeycloakBootstrap(string realmName, TimeSpan accessTokenLifespan)
{
  public async Task Execute(
    IKeycloakAdminClient adminClient,
    string clientId,
    string artistRole,
    string adminRole)
  {
    var adminToken = await adminClient.GetAdminToken("admin", "admin");

    await adminClient.CreateRealm(
      adminToken,
      new
      {
        realm = realmName,
        enabled = true,
        verifyEmail = false,
        accessTokenLifespan = accessTokenLifespan.TotalSeconds,
        requiredCredentials = new[] { "password" }
      });

    await adminClient.CreateClient(
      adminToken,
      realmName,
      new
      {
        clientId = clientId,
        enabled = true,
        publicClient = true,
        directAccessGrantsEnabled = true,
        standardFlowEnabled = true,
        fullScopeAllowed = true,
        protocol = "openid-connect"
      });

    await adminClient.CreateRole(adminToken, realmName, artistRole);
    await adminClient.CreateRole(adminToken, realmName, adminRole);

    await CreateUser(adminClient, adminToken, realmName, KeycloakConstants.ArtistUser, [artistRole]);
    await CreateUser(adminClient, adminToken, realmName, KeycloakConstants.AdminUser, [adminRole, artistRole]);
    await CreateUser(adminClient, adminToken, realmName, KeycloakConstants.UnprivilegedUser, []);
  }

  private static async Task CreateUser(IKeycloakAdminClient adminClient, string adminToken, string realmName, string username,
    string[] roles)
  {
    var userId = await adminClient.CreateUser(
      adminToken,
      realmName,
      new
      {
        username = username,
        enabled = true,
        emailVerified = true,
        firstName = username,
        lastName = "Test",
        email = $"{username}@example.com",
        requiredActions = Array.Empty<string>()
      });

    await adminClient.ResetPassword(adminToken, realmName, userId, KeycloakConstants.Password);

    foreach (var roleName in roles)
    {
      var role = await adminClient.GetRole(adminToken, realmName, roleName);
      await adminClient.AssignRoleToUser(adminToken, realmName, userId, role);
    }
  }
}
