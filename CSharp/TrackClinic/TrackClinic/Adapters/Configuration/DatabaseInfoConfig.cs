namespace TrackClinic.Adapters.Configuration;

public record DatabaseInfoConfig
{
  public required string ConnectionString { get; init; }
}