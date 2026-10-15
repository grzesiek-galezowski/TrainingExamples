using Serilog.Events;

namespace TrackClinic;

public record LoggingConfig
{
  public LogEventLevel DefaultLevel { get; init; } = LogEventLevel.Information;
  public LogEventLevel MicrosoftOverride { get; init; } = LogEventLevel.Warning;
}
