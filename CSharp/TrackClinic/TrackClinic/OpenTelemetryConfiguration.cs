using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace TrackClinic;

public static class OpenTelemetryConfiguration
{
  public static void AddTo(WebApplicationBuilder builder)
  {
    var resourceBuilder = ResourceBuilder.CreateDefault()
      .AddService(builder.Environment.ApplicationName);

    builder.Services.AddOpenTelemetry()
      .WithTracing(tracing => tracing
        .SetResourceBuilder(resourceBuilder)
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddZipkinExporter())
      .WithMetrics(metrics => metrics
        .SetResourceBuilder(resourceBuilder)
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddRuntimeInstrumentation()
        .AddPrometheusExporter());
  }

  public static void Map(WebApplication app)
  {
    app.MapPrometheusScrapingEndpoint();
  }
}
