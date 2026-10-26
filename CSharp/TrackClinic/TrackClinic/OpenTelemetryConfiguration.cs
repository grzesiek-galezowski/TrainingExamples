using OpenTelemetry.Exporter;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using TrackClinic.Adapters.Configuration;

namespace TrackClinic;

public static class OpenTelemetryConfiguration
{
  public static void AddTo(WebApplicationBuilder builder, OpenTelemetryConfig config)
  {
    var resourceBuilder = ResourceBuilder.CreateDefault()
      .AddService(builder.Environment.ApplicationName);

    builder.Services.AddOpenTelemetry()
      .WithTracing(tracing => tracing
        .SetResourceBuilder(resourceBuilder)
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddOtlpExporter(options =>
        {
          options.Protocol = config.Protocol;

          var endpointString = config.Endpoint.ToString();
          if (config.Protocol == OtlpExportProtocol.HttpProtobuf)
          {
            options.Endpoint = new Uri(endpointString.EndsWith("/v1/traces") ? endpointString : $"{endpointString.TrimEnd('/')}/v1/traces");
          }
          else
          {
            options.Endpoint = config.Endpoint;
          }

          if (config.ScheduledDelayMilliseconds.HasValue)
          {
            options.BatchExportProcessorOptions.ScheduledDelayMilliseconds = config.ScheduledDelayMilliseconds.Value;
          }
        }))
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
