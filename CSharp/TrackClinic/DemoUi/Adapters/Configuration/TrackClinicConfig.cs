namespace DemoUi.Adapters.Configuration;

public record TrackClinicConfig
{
  public required Uri BaseUrl { get; init; }
  public int TimeoutSeconds { get; init; } = 15;
}
