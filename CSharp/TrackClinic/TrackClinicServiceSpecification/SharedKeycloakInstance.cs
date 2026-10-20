using System.Net.Http.Headers;
using System.Text.Json;
using Testcontainers.Keycloak;

namespace TrackClinicServiceSpecification;

public static class SharedKeycloakInstance
{
  private const string ArtistRole = "artist";
  private const string AdminRole = "admin";

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

  public static string GetAuthority() => $"{KeycloakContainer.GetBaseAddress()}realms/track-clinic";

  public static string GetMetadataAddress() =>
    $"{KeycloakContainer.GetBaseAddress()}realms/track-clinic/.well-known/openid-configuration";

  private static async Task ConfigureKeycloak()
  {
    using var httpClient = new HttpClient();
    var adminToken = await GetAdminToken(httpClient);
    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", adminToken);

    var baseAddress = KeycloakContainer.GetBaseAddress().TrimEnd('/');

    await CreateRealm(httpClient, baseAddress);
    await CreateClient(httpClient, baseAddress);

    await CreateRole(httpClient, baseAddress, ArtistRole);
    await CreateRole(httpClient, baseAddress, AdminRole);

    // Create Users
    await CreateUser(httpClient, baseAddress, "artist-user", [ArtistRole]);
    await CreateUser(httpClient, baseAddress, "admin-user", [AdminRole, ArtistRole]);
    await CreateUser(httpClient, baseAddress, "unprivileged-user", []);
  }

  private static async Task CreateRole(HttpClient httpClient, string baseAddress, string role)
  {
    (await httpClient.PostAsJsonAsync($"{baseAddress}/admin/realms/track-clinic/roles", new { name = role }))
      .EnsureSuccessStatusCode();
  }

  private static async Task CreateClient(HttpClient httpClient, string baseAddress)
  {
    var clientResponse = await httpClient.PostAsJsonAsync(
      $"{baseAddress}/admin/realms/track-clinic/clients",
      new
      {
        clientId = "track-clinic-api",
        enabled = true,
        publicClient = true,
        directAccessGrantsEnabled = true,
        standardFlowEnabled = true,
        fullScopeAllowed = true,
        protocol = "openid-connect"
      });
    clientResponse.EnsureSuccessStatusCode();
  }

  private static async Task CreateRealm(HttpClient httpClient, string baseAddress)
  {
    var realmResponse = await httpClient.PostAsJsonAsync(
      $"{baseAddress}/admin/realms",
      new
      {
        realm = "track-clinic", //bug duplication
        enabled = true,
        verifyEmail = false,
        requiredCredentials = new[] { "password" }
      });
    realmResponse.EnsureSuccessStatusCode();
  }

  private static async Task CreateUser(HttpClient httpClient, string baseAddress, string username, string[] roles)
  {
    var userResponse = await httpClient.PostAsJsonAsync(
      $"{baseAddress}/admin/realms/track-clinic/users",
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

    var location = userResponse.Headers.Location;
    var userId = location!.Segments.Last();

    // Set Password
    var passwordResponse = await httpClient.PutAsJsonAsync(
      $"{baseAddress}/admin/realms/track-clinic/users/{userId}/reset-password",
      new
      {
        type = "password",
        value = "password", //bug duplication
        temporary = false
      });
    passwordResponse.EnsureSuccessStatusCode();

    // Remove any default required actions that might have been added
    await httpClient.PutAsJsonAsync(
      $"{baseAddress}/admin/realms/track-clinic/users/{userId}",
      new
      {
        requiredActions = Array.Empty<string>()
      });

    foreach (var roleName in roles)
    {
      var roleResponse = await httpClient.GetAsync($"{baseAddress}/admin/realms/track-clinic/roles/{roleName}");
      roleResponse.EnsureSuccessStatusCode();
      var role = await roleResponse.Content.ReadFromJsonAsync<JsonElement>();

      var mappingResponse = await httpClient.PostAsJsonAsync(
        $"{baseAddress}/admin/realms/track-clinic/users/{userId}/role-mappings/realm",
        new[] { role });
      mappingResponse.EnsureSuccessStatusCode();
    }
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
      $"{KeycloakContainer.GetBaseAddress()}realms/track-clinic/protocol/openid-connect/token",
      new FormUrlEncodedContent(
        new Dictionary<string, string>
        {
          ["grant_type"] = "password",
          ["client_id"] = "track-clinic-api",
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
}