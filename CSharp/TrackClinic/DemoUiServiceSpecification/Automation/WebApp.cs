using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.InMemory;
using ServiceBuildingBlocks;
using ServiceSpecificationBuildingBlocks.Keycloak;
using ServiceSpecificationBuildingBlocks.Zipkin;

namespace DemoUiServiceSpecification.Automation;

internal class WebApp : WebApplicationFactory<Program>
{
  private readonly Dictionary<string, string?> _configOverrides;

  private readonly InMemorySink _inMemorySink;

  public WebApp(string trackClinicBaseUrl, string keycloakRealmName, InMemorySink inMemorySink)
  {
    _inMemorySink = inMemorySink;
    _configOverrides = new Dictionary<string, string?>
    {
      ["Logging:DefaultLevel"] = "Information",
      ["Logging:MicrosoftOverride"] = "Warning",
      ["OpenTelemetry:Protocol"] = "HttpProtobuf",
      ["OpenTelemetry:Endpoint"] = SharedZipkinInstance.GetOtlpHttpEndpoint(),
      ["OpenTelemetry:ScheduledDelayMilliseconds"] = "100",
      ["TrackClinic:BaseUrl"] = trackClinicBaseUrl,
      ["TrackClinic:TimeoutSeconds"] = "15",
      ["Keycloak:Authority"] = SharedKeycloakInstance.GetAuthority(keycloakRealmName),
      ["Keycloak:MetadataAddress"] = SharedKeycloakInstance.GetMetadataAddress(keycloakRealmName),
      ["Keycloak:RequireHttpsMetadata"] = "false",
    };
    UseKestrel(0); // for Playwright to work, a real HTTP server is needed
  }

  protected override void ConfigureWebApplicationBuilder(IHostApplicationBuilder builder)
  {
    builder.Configuration.AddInMemoryCollection(_configOverrides);
    base.ConfigureWebApplicationBuilder(builder);
  }

  protected override IHost CreateHost(IHostBuilder builder)
  {
    builder.UseSerilog(LoggingConfigurations.ConfigureServiceLogging(
        new LoggerConfiguration(), LogEventLevel.Information, LogEventLevel.Information)
      .WriteTo.Sink(_inMemorySink)
      .CreateLogger(), dispose: true);
    var host = base.CreateHost(builder);
    return host;
  }
}
