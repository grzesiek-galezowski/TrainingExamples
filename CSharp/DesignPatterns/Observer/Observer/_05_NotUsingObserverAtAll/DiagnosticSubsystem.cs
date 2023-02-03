namespace Observer._05_NotUsingObserverAtAll;

internal class DiagnosticSubsystem : IDiagnosticSubsystem
{
  private readonly ITelemetry _telemetry;
  private readonly IThresholdAlertDetection _alertDetection;
  private readonly ICleanupHistory _cleanupHistory;

  public DiagnosticSubsystem(
    ITelemetry telemetry,
    IThresholdAlertDetection alertDetection,
    ICleanupHistory cleanupHistory)
  {
    _telemetry = telemetry;
    _alertDetection = alertDetection;
    _cleanupHistory = cleanupHistory;
  }

  public void HandleFilesCleanupSuccessful(int filesCount)
  {
    _telemetry.Report(filesCount);
    _alertDetection.Check(filesCount);
    _cleanupHistory.Save(filesCount);
  }
}