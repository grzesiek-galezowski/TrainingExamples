using Serilog.Events;

namespace TrackClinic;

public record LoggingConfig
{
  public required LogEventLevel DefaultLevel { get; init; } = LogEventLevel.Information;
  public required LogEventLevel MicrosoftOverride { get; init; } = LogEventLevel.Warning;
}
