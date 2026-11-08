using DemoUi;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using ServiceSpecificationBuildingBlocks.Keycloak;
using ServiceSpecificationBuildingBlocks.Zipkin;

namespace DemoUiServiceSpecification.Automation;

internal class WebApp(string trackClinicBaseUrl, string keycloakRealmName) : WebApplicationFactory<Program>
{
  private readonly Dictionary<string, string?> _configOverrides = new()
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

  protected override void ConfigureWebApplicationBuilder(IHostApplicationBuilder builder)
  {
    builder.Configuration.AddInMemoryCollection(_configOverrides);
    base.ConfigureWebApplicationBuilder(builder);
  }

  protected override IHost CreateHost(IHostBuilder builder)
  {
    builder.UseSerilog(LoggingConfigurations.ConfigureServiceLogging(
        new LoggerConfiguration(), LogEventLevel.Information, LogEventLevel.Information)
      .CreateLogger(), dispose: true);
    var host = base.CreateHost(builder);
    return host;
  }
}
