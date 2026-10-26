using Core.NullableReferenceTypesExtensions;

namespace TrackClinic.Adapters.Configuration;

public record AppConfig
{
  public required DatabaseInfoConfig DatabaseInfo { get; init; }
  public required LoggingConfig Logging { get; init; }
  public required KeycloakConfig Keycloak { get; init; }
  public required OpenTelemetryConfig OpenTelemetry { get; init; }

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
    ArgumentNullException.ThrowIfNull(Keycloak);
    ArgumentNullException.ThrowIfNull(OpenTelemetry);
    ArgumentNullException.ThrowIfNull(DatabaseInfo.ConnectionString);
    ArgumentNullException.ThrowIfNull(Keycloak.Authority);
    ArgumentNullException.ThrowIfNull(Keycloak.MetadataAddress);
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