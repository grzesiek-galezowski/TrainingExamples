using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace TrackClinic.ContainerConfiguration;

public static class HealthChecksConfiguration
{
  private const string ReadyTagName = "ready";

  public static void AddTo(WebApplicationBuilder builder)
  {
    builder.Services.AddHealthChecks().AddMongoDb(
      ctx => ctx.GetRequiredService<ServiceLogicRoot>().MongoClient,
      tags: [ReadyTagName])
      .AddCheck<KeycloakHealthCheck>("keycloak");
  } //bug add keycloak

  public static void Map(WebApplication app)
  {
    app.MapHealthChecks(
      "/health/live",
      new HealthCheckOptions
      {
        Predicate = _ => false //only report that the service is live
      });
    app.MapHealthChecks(
      "/health/ready",
      new HealthCheckOptions
      {
        Predicate = check => check.Tags.Contains(ReadyTagName)
      });
  }
}

public sealed class KeycloakHealthCheck : IHealthCheck
{
  private readonly HttpClient _httpClient;

  public KeycloakHealthCheck(HttpClient httpClient)
  {
    _httpClient = httpClient;
  }

  public async Task<HealthCheckResult> CheckHealthAsync(
    HealthCheckContext context,
    CancellationToken cancellationToken = default)
  {
    try
    {
      var response = await _httpClient.GetAsync(
        "/realms/track-clinic/.well-known/openid-configuration",
        cancellationToken);

      if (!response.IsSuccessStatusCode)
        return HealthCheckResult.Unhealthy(
          $"Discovery endpoint returned {response.StatusCode}");

      return HealthCheckResult.Healthy();
    }
    catch (Exception ex)
    {
      return HealthCheckResult.Unhealthy(
        "Cannot reach Keycloak",
        ex);
    }
  }
}