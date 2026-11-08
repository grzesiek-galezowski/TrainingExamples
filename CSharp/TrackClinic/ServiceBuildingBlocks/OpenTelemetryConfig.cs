using OpenTelemetry.Exporter;

namespace ServiceBuildingBlocks;

public record OpenTelemetryConfig
{
  public required Uri Endpoint { get; init; }
  public OtlpExportProtocol Protocol { get; init; } = OtlpExportProtocol.HttpProtobuf;
  public int? ScheduledDelayMilliseconds { get; init; }

  public void Validate()
  {
    ArgumentNullException.ThrowIfNull(Endpoint);
    ArgumentNullException.ThrowIfNull(Protocol);
  }
}
