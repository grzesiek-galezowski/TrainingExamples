using Core.NullableReferenceTypesExtensions;
using OpenTelemetry.Exporter;
using ServiceBuildingBlocks;

namespace TrackClinic.Adapters.Configuration;

public record AppConfig
{
  public required DatabaseInfoConfig DatabaseInfo { get; init; }
  public LoggingConfig Logging { get; init; } = new();
  public KeycloakConfig Keycloak { get; init; } = new()
  {
    Authority = "http://localhost:8080/realms/track-clinic",
    MetadataAddress = "http://localhost:8080/realms/track-clinic/.well-known/openid-configuration",
    RequireHttpsMetadata = false
  };

  public OpenTelemetryConfig OpenTelemetry { get; init; } = new()
  {
    Endpoint = new Uri("http://localhost:9411/v1/traces"),
    Protocol = OtlpExportProtocol.HttpProtobuf
  };

  public static AppConfig From(ConfigurationManager builderConfiguration)
  {
    var appConfig = builderConfiguration.Get<AppConfig>().OrThrow();
    appConfig.Assert();
    return appConfig;
  }

  private void Assert()
  {
    ArgumentNullException.ThrowIfNull(Logging);
    ArgumentNullException.ThrowIfNull(DatabaseInfo);
    ArgumentException.ThrowIfNullOrWhiteSpace(DatabaseInfo.ConnectionString);
    ArgumentNullException.ThrowIfNull(Keycloak);
    Keycloak.Validate();
    ArgumentNullException.ThrowIfNull(OpenTelemetry);
    ArgumentNullException.ThrowIfNull(OpenTelemetry.Endpoint);
    if (!OpenTelemetry.Endpoint.IsAbsoluteUri)
    {
      throw new ArgumentException("Endpoint must be an absolute URI", nameof(OpenTelemetry.Endpoint));
    }
    if (OpenTelemetry.ScheduledDelayMilliseconds is <= 0)
    {
      throw new ArgumentOutOfRangeException(
        nameof(OpenTelemetry.ScheduledDelayMilliseconds),
        "ScheduledDelayMilliseconds must be greater than 0");
    }
  }
}