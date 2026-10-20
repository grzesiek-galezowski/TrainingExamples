namespace TrackClinic;

public record DatabaseInfoConfig
{
  public required string ConnectionString { get; init; }
}