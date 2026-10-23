using System.Text.Json;
using Flurl;
using Flurl.Http;

namespace TrackClinicServiceSpecification.Automation.Keycloak;

public class KeycloakClient(string baseAddress) : IKeycloakClient
{
  private readonly string _baseAddress = baseAddress.TrimEnd('/');

  public async Task<string> GetAdminToken(string username, string password)
  {
    var response = await Request("realms", "master", "protocol", "openid-connect", "token")
      .PostUrlEncodedAsync(new
      {
        grant_type = "password",
        client_id = "admin-cli",
        username,
        password
      });

    var content = await response.GetJsonAsync<JsonElement>();
    return content.GetProperty("access_token").GetString()!;
  }

  public async Task<string> GetToken(string realmName, string clientId, string username, string password)
  {
    var response = await Request("realms", realmName, "protocol", "openid-connect", "token")
      .PostUrlEncodedAsync(new
      {
        grant_type = "password",
        client_id = clientId,
        username,
        password
      });

    var content = await response.GetJsonAsync<JsonElement>();
    return content.GetProperty("access_token").GetString()!;
  }

  public async Task CreateRealm(string adminToken, object realmData)
  {
    await Request("admin", "realms")
      .WithOAuthBearerToken(adminToken)
      .PostJsonAsync(realmData);
  }

  public async Task CreateClient(string adminToken, string realmName, object clientData)
  {
    await Request("admin", "realms", realmName, "clients")
      .WithOAuthBearerToken(adminToken)
      .PostJsonAsync(clientData);
  }

  public async Task CreateRole(string adminToken, string realmName, string roleName)
  {
    await Request("admin", "realms", realmName, "roles")
      .WithOAuthBearerToken(adminToken)
      .PostJsonAsync(new { name = roleName });
  }

  public async Task<string> CreateUser(string adminToken, string realmName, object userData)
  {
    var response = await Request("admin", "realms", realmName, "users")
      .WithOAuthBearerToken(adminToken)
      .PostJsonAsync(userData);

    return response.Headers.FirstOrDefault("Location")!.Split('/').Last();
  }

  public async Task ResetPassword(string adminToken, string realmName, string userId, string password)
  {
    await Request("admin", "realms", realmName, "users", userId, "reset-password")
      .WithOAuthBearerToken(adminToken)
      .PutJsonAsync(new
      {
        type = "password",
        value = password,
        temporary = false
      });
  }

  public async Task AssignRoleToUser(string adminToken, string realmName, string userId, JsonElement role)
  {
    await Request("admin", "realms", realmName, "users", userId, "role-mappings", "realm")
      .WithOAuthBearerToken(adminToken)
      .PostJsonAsync(new[] { role });
  }

  public async Task<JsonElement> GetRole(string adminToken, string realmName, string roleName)
  {
    return await Request("admin", "realms", realmName, "roles", roleName)
      .WithOAuthBearerToken(adminToken)
      .GetJsonAsync<JsonElement>();
  }

  private Url Request(params object[] pathSegments) =>
    _baseAddress.AppendPathSegments(pathSegments);
}
