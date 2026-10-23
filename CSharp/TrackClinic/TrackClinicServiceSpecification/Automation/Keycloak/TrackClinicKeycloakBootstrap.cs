using TrackClinic;

namespace TrackClinicServiceSpecification.Automation.Keycloak;

public class TrackClinicKeycloakBootstrap(IKeycloakClient client)
{
  public async Task Execute(
    string realmName, 
    string clientId, 
    string artistRole, 
    string adminRole)
  {
    var adminToken = await client.GetAdminToken("admin", "admin");

    await client.CreateRealm(adminToken, new
    {
      realm = realmName,
      enabled = true,
      verifyEmail = false,
      requiredCredentials = new[] { "password" }
    });

    await client.CreateClient(adminToken, realmName, new
    {
      clientId = clientId,
      enabled = true,
      publicClient = true,
      directAccessGrantsEnabled = true,
      standardFlowEnabled = true,
      fullScopeAllowed = true,
      protocol = "openid-connect"
    });

    await client.CreateRole(adminToken, realmName, artistRole);
    await client.CreateRole(adminToken, realmName, adminRole);

    await CreateUser(adminToken, realmName, "artist-user", [artistRole]);
    await CreateUser(adminToken, realmName, "admin-user", [adminRole, artistRole]);
    await CreateUser(adminToken, realmName, "unprivileged-user", []);
  }

  private async Task CreateUser(string adminToken, string realmName, string username, string[] roles)
  {
    var userId = await client.CreateUser(adminToken, realmName, new
    {
      username = username,
      enabled = true,
      emailVerified = true,
      firstName = username,
      lastName = "Test",
      email = $"{username}@example.com",
      requiredActions = Array.Empty<string>()
    });

    await client.ResetPassword(adminToken, realmName, userId, KeycloakConstants.Password);

    foreach (var roleName in roles)
    {
      var role = await client.GetRole(adminToken, realmName, roleName);
      await client.AssignRoleToUser(adminToken, realmName, userId, role);
    }
  }
}
