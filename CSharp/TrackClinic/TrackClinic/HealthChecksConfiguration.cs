using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace TrackClinic;

public static class HealthChecksConfiguration
{
  private const string ReadyTagName = "ready";

  public static void AddTo(WebApplicationBuilder builder)
  {
    builder.Services.AddHealthChecks().AddMongoDb(
      ctx => ctx.GetRequiredService<ServiceLogicRoot>().MongoClient,
      tags: [ReadyTagName]);
  }

  public static void Map(WebApplication app)
  {
    //bug better healthcheck tests here. Without the containers healthy should pass but ready should fail.
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