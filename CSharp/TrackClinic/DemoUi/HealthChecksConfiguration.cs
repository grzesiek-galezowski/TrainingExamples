using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace DemoUi;

public static class HealthChecksConfiguration
{
  private const string ReadyTagName = "ready";

  public static void AddTo(WebApplicationBuilder builder)
  {
    builder.Services.AddHealthChecks()
      .AddCheck("self", () => HealthCheckResult.Healthy(), tags: [ReadyTagName]);
  }

  public static void Map(WebApplication app)
  {
    app.MapHealthChecks(
      "/health/live",
      new HealthCheckOptions
      {
        Predicate = _ => false
      });
    app.MapHealthChecks(
      "/health/ready",
      new HealthCheckOptions
      {
        Predicate = check => check.Tags.Contains(ReadyTagName)
      });
  }
}
