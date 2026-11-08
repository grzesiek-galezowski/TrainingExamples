using System.Text.Json;
using AwesomeAssertions.Extensions;
using Core.NullableReferenceTypesExtensions;
using Flurl.Http;

namespace DemoUi.Adapters.Keycloak;

public class KeycloakTokenClient
{
  // bug cache the token?
  public async Task<string> GetPasswordToken(
    Uri metadataAddress,
    string clientId,
    string username,
    string password,
    CancellationToken cancellationToken)
  {
    var tokenEndpoint = await ResolveTokenEndpoint(metadataAddress, cancellationToken);

    var response = await tokenEndpoint
      .AllowAnyHttpStatus()
      .WithTimeout(5.Seconds())
      .PostUrlEncodedAsync(
        new
        {
          grant_type = "password",
          client_id = clientId,
          username,
          password
        },
        cancellationToken: cancellationToken);

    AssertIsSuccess(
      response,
      $"Token request failed with status code {response.StatusCode} {await response.GetStringAsync()}");

    return await ExtractAccessTokenFrom(response);
  }

  private async Task<string> ResolveTokenEndpoint(Uri metadataAddress, CancellationToken ct)
  {
    return ExtractTokenEndpointFrom(
      await metadataAddress
        .WithTimeout(5.Seconds())
        .GetJsonAsync<JsonElement>(cancellationToken: ct));
  }

  private static string ExtractTokenEndpointFrom(JsonElement json)
  {
    return json.GetProperty("token_endpoint").GetString() ?? throw new InvalidOperationException("No token_endpoint in discovery document.");
  }

  private static async Task<string> ExtractAccessTokenFrom(IFlurlResponse response)
  {
    var json = await response.GetJsonAsync<JsonElement>();
    return json.GetProperty("access_token").GetString().OrThrow();
  }

  private static void AssertIsSuccess(IFlurlResponse response, string message)
  {
    if (!response.ResponseMessage.IsSuccessStatusCode)
    {
      throw new InvalidOperationException(
        message);
    }
  }
}
