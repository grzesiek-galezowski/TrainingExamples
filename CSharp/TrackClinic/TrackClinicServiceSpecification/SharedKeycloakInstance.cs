using System.Net.Http.Headers;
using System.Text.Json;
using Testcontainers.Keycloak;

namespace TrackClinicServiceSpecification;

public static class SharedKeycloakInstance
{
  private const string ArtistRole = "artist";
  private const string AdminRole = "admin";
  private const string RealmName = "track-clinic";
  private const string ClientId = "track-clinic-api";

  private static readonly KeycloakContainer KeycloakContainer = new KeycloakBuilder("quay.io/keycloak/keycloak:26.0")
    .Build();

  public static async Task Start()
  {
    await KeycloakContainer.StartAsync();
    await ConfigureKeycloak();
  }

  public static async Task Stop()
  {
    await KeycloakContainer.StopAsync();
  }

  public static string GetAuthority() => $"{KeycloakContainer.GetBaseAddress()}realms/{RealmName}";

  public static string GetMetadataAddress() =>
    $"{KeycloakContainer.GetBaseAddress()}realms/{RealmName}/.well-known/openid-configuration";

  private static async Task ConfigureKeycloak()
  {
    using var httpClient = new HttpClient();
    var adminToken = await GetAdminToken(httpClient);
    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", adminToken);

    await CreateRealm(httpClient);
    await CreateClient(httpClient);

    await CreateRole(httpClient, ArtistRole);
    await CreateRole(httpClient, AdminRole);

    // Create Users
    await CreateUser(httpClient, "artist-user", [ArtistRole]);
    await CreateUser(httpClient, "admin-user", [AdminRole, ArtistRole]);
    await CreateUser(httpClient, "unprivileged-user", []);
  }

  private static async Task CreateRole(HttpClient httpClient, string role)
  {
    (await httpClient.PostAsJsonAsync(RealmUrl("roles"), new { name = role }))
      .EnsureSuccessStatusCode();
  }

  private static async Task CreateClient(HttpClient httpClient)
  {
    var clientResponse = await httpClient.PostAsJsonAsync(
      RealmUrl("clients"),
      new
      {
        clientId = ClientId,
        enabled = true,
        publicClient = true,
        directAccessGrantsEnabled = true,
        standardFlowEnabled = true,
        fullScopeAllowed = true,
        protocol = "openid-connect"
      });
    clientResponse.EnsureSuccessStatusCode();
  }

  private static async Task CreateRealm(HttpClient httpClient)
  {
    var realmResponse = await httpClient.PostAsJsonAsync(
      RealmsUrl(),
      new
      {
        realm = RealmName, //bug duplication
        enabled = true,
        verifyEmail = false,
        requiredCredentials = new[] { "password" }
      });
    realmResponse.EnsureSuccessStatusCode();
  }

  private static async Task CreateUser(HttpClient httpClient, string username, string[] roles)
  {
    var userResponse = await CreateUserEntry(httpClient, username);

    var userId = userResponse.Headers.Location!.Segments.Last();

    await ResetPassword(httpClient, userId);

    foreach (var roleName in roles)
    {
      var role = await GetRole(httpClient, roleName);
      await AssignRoleToUser(httpClient, userId, role);
    }
  }

  private static async Task AssignRoleToUser(HttpClient httpClient, string userId, JsonElement role)
  {
    var mappingResponse = await httpClient.PostAsJsonAsync(
      RealmUrl($"users/{userId}/role-mappings/realm"),
      new[] { role });
    mappingResponse.EnsureSuccessStatusCode();
  }

  private static async Task<JsonElement> GetRole(HttpClient httpClient, string roleName)
  {
    var roleResponse = await httpClient.GetAsync(RealmUrl($"/roles/{roleName}"));
    roleResponse.EnsureSuccessStatusCode();
    var role = await roleResponse.Content.ReadFromJsonAsync<JsonElement>();
    return role;
  }

  private static async Task ResetPassword(HttpClient httpClient, string userId)
  {
    // Set Password
    var passwordResponse = await httpClient.PutAsJsonAsync(
      RealmUrl($"users/{userId}/reset-password"),
      new
      {
        type = "password",
        value = "password", //bug duplication
        temporary = false
      });
    passwordResponse.EnsureSuccessStatusCode();
  }

  private static async Task<HttpResponseMessage> CreateUserEntry(HttpClient httpClient, string username)
  {
    var userResponse = await httpClient.PostAsJsonAsync(
      RealmUrl("users"),
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
    userResponse.EnsureSuccessStatusCode();
    return userResponse;
  }

  private static async Task<string> GetAdminToken(HttpClient httpClient)
  {
    var response = await httpClient.PostAsync(
      $"{KeycloakContainer.GetBaseAddress()}realms/master/protocol/openid-connect/token",
      new FormUrlEncodedContent(
        new Dictionary<string, string>
        {
          ["grant_type"] = "password",
          ["client_id"] = "admin-cli",
          ["username"] = AdminRole,
          ["password"] = AdminRole
        }));

    if (!response.IsSuccessStatusCode)
    {
      var error = await response.Content.ReadAsStringAsync();
      throw new Exception($"Failed to get admin token: {response.StatusCode} - {error}");
    }

    var content = await response.Content.ReadFromJsonAsync<JsonElement>();
    return content.GetProperty("access_token").GetString()!;
  }

  public static async Task<string> GetToken(string username, string password)
  {
    using var httpClient = new HttpClient(); //bug
    var response = await httpClient.PostAsync(
      $"{KeycloakContainer.GetBaseAddress()}realms/{RealmName}/protocol/openid-connect/token",
      new FormUrlEncodedContent(
        new Dictionary<string, string>
        {
          ["grant_type"] = "password",
          ["client_id"] = ClientId,
          ["username"] = username,
          ["password"] = password
        }));

    if (!response.IsSuccessStatusCode)
    {
      var error = await response.Content.ReadAsStringAsync();
      throw new Exception($"Failed to get token for {username}: {response.StatusCode} - {error}");
    }

    var content = await response.Content.ReadFromJsonAsync<JsonElement>();
    return content.GetProperty("access_token").GetString()!;
  }

  private static string RealmUrl(string endpointName)
  {
    return $"{BaseAddress()}/admin/realms/{RealmName}/{endpointName}";
  }
  
  private static string RealmsUrl()
  {
    return $"{BaseAddress()}/admin/realms";
  }

  private static string BaseAddress()
  {
    return KeycloakContainer.GetBaseAddress().TrimEnd('/');
  }

}