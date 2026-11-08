using System.Text.Json;
using Flurl;
using Flurl.Http;

namespace ServiceSpecificationBuildingBlocks.Keycloak;

public class KeycloakAdminClient(string baseAddress) : IKeycloakAdminClient
{
  private readonly string _baseAddress = baseAddress.TrimEnd('/');

  public async Task<string> GetAdminToken(string username, string password)
  {
    return await GetToken("master", "admin-cli", username, password);
  }

  public async Task<string> GetToken(string realmName, string clientId, string username, string password)
  {
    var response = await TokenRequestForRealm(realmName)
      .PostUrlEncodedAsync(PasswordTokenRequest(username, password, clientId));

    var content = await response.GetJsonAsync<JsonElement>();
    return content.GetProperty("access_token").GetString()!;
  }

  public async Task CreateRealm(string adminToken, object realmData)
  {
    await AdminRequest()
      .WithOAuthBearerToken(adminToken)
      .PostJsonAsync(realmData);
  }

  public async Task CreateClient(string adminToken, string realmName, object clientData)
  {
    await AdminRequest().AppendPathSegments(realmName, "clients")
      .WithOAuthBearerToken(adminToken)
      .PostJsonAsync(clientData);
  }

  public async Task CreateRole(string adminToken, string realmName, string roleName)
  {
    await AdminRequest().AppendPathSegments(realmName, "roles")
      .WithOAuthBearerToken(adminToken)
      .PostJsonAsync(new { name = roleName });
  }

  public async Task<string> CreateUser(string adminToken, string realmName, object userData)
  {
    var response = await AdminRequest().AppendPathSegments(realmName, "users")
      .WithOAuthBearerToken(adminToken)
      .PostJsonAsync(userData);

    return response.Headers.FirstOrDefault("Location")!.Split('/').Last();
  }

  public async Task ResetPassword(string adminToken, string realmName, string userId, string password)
  {
    await AdminRequest().AppendPathSegments(realmName, "users", userId, "reset-password")
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
    await AdminRequest().AppendPathSegments(realmName, "users", userId, "role-mappings", "realm")
      .WithOAuthBearerToken(adminToken)
      .PostJsonAsync(new[] { role });
  }

  public async Task<JsonElement> GetRole(string adminToken, string realmName, string roleName)
  {
    return await AdminRequest().AppendPathSegments(realmName, "roles", roleName)
      .WithOAuthBearerToken(adminToken)
      .GetJsonAsync<JsonElement>();
  }

  private Url AdminRequest() => Request("admin", "realms");

  private Url Request(params object[] pathSegments) =>
    _baseAddress.AppendPathSegments(pathSegments);

  private Url TokenRequestForRealm(string realmName) => 
    Request("realms", realmName, "protocol", "openid-connect", "token");

  private static object PasswordTokenRequest(string username, string password, string clientId)
  {
    return new
    {
      grant_type = "password",
      client_id = clientId,
      username,
      password
    };
  }
}
