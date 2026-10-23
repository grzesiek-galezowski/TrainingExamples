using System.Text.Json;

namespace TrackClinicServiceSpecification.Automation.Keycloak;

public interface IKeycloakClient
{
  Task<string> GetAdminToken(string username, string password);
  Task<string> GetToken(string realmName, string clientId, string username, string password);
  Task CreateRealm(string adminToken, object realmData);
  Task CreateClient(string adminToken, string realmName, object clientData);
  Task CreateRole(string adminToken, string realmName, string roleName);
  Task<string> CreateUser(string adminToken, string realmName, object userData);
  Task ResetPassword(string adminToken, string realmName, string userId, string password);
  Task AssignRoleToUser(string adminToken, string realmName, string userId, JsonElement role);
  Task<JsonElement> GetRole(string adminToken, string realmName, string roleName);
}
