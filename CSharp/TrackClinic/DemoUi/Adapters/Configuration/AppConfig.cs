using Core.NullableReferenceTypesExtensions;
using ServiceBuildingBlocks;

namespace DemoUi.Adapters.Configuration;

public record AppConfig
{
  public LoggingConfig Logging { get; init; } = new();
  public OpenTelemetryConfig OpenTelemetry { get; init; } = new()
  {
    Endpoint = new Uri("http://localhost:9411/v1/traces"),
    Protocol = global::OpenTelemetry.Exporter.OtlpExportProtocol.HttpProtobuf
  };

  public TrackClinicConfig TrackClinic { get; init; } = new()
  {
    BaseUrl = new Uri("http://TrackClinicService")
  };
  public KeycloakConfig Keycloak { get; init; } = new()
  {
    Authority = new Uri("http://localhost:8080/realms/track-clinic"),
    MetadataAddress = new Uri("http://localhost:8080/realms/track-clinic/.well-known/openid-configuration")
  };

  public static AppConfig From(IConfiguration builderConfiguration)
  {
    var appConfig = builderConfiguration.Get<AppConfig>().OrThrow();
    appConfig.Assert();
    return appConfig;
  }

  public void Assert()
  {
    ArgumentNullException.ThrowIfNull(Logging);
    ArgumentNullException.ThrowIfNull(OpenTelemetry);
    OpenTelemetry.Validate();
    ArgumentNullException.ThrowIfNull(TrackClinic);
    ArgumentNullException.ThrowIfNull(TrackClinic.BaseUrl);
    ArgumentNullException.ThrowIfNull(Keycloak);
    Keycloak.Validate();
    if (!TrackClinic.BaseUrl.IsAbsoluteUri)
    {
      throw new ArgumentException("BaseUrl must be an absolute URI", nameof(TrackClinic.BaseUrl));
    }
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
