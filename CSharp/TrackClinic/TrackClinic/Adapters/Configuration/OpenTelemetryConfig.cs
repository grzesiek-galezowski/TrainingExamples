using OpenTelemetry.Exporter;

namespace TrackClinic.Adapters.Configuration;

public record OpenTelemetryConfig
{
  public required Uri Endpoint { get; init; }
  public OtlpExportProtocol Protocol { get; init; } = OtlpExportProtocol.HttpProtobuf;
  public int? ScheduledDelayMilliseconds { get; init; }
}
