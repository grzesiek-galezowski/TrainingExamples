namespace TrackClinic.Adapters.Configuration;

public record KeycloakConfig
{
  public required string Authority { get; init; }
  public required string MetadataAddress { get; init; }
  public bool RequireHttpsMetadata { get; init; } = true;
  public TimeSpan? GracePeriod { get; init; }
}