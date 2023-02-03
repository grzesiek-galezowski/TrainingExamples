using Observer.Common;

namespace Observer._05_NotUsingObserverAtAll;

internal class CleanupJob
{
  private readonly ICleanedUpDir _cleanedUpDir;
  private readonly ICleanUpProcedure _cleanupProcedure;
  private readonly IDiagnosticSubsystem _diagnosticSubsystem;

  public CleanupJob(
    ICleanedUpDir cleanedUpDir,
    ICleanUpProcedure cleanupProcedure, 
    IDiagnosticSubsystem diagnosticSubsystem)
  {
    _cleanedUpDir = cleanedUpDir;
    _cleanupProcedure = cleanupProcedure;
    _diagnosticSubsystem = diagnosticSubsystem;
  }

  public void Run()
  {
    var files = _cleanedUpDir.GetFilesToCleanup();
    _cleanupProcedure.RunOn(files);
    _diagnosticSubsystem.HandleFilesCleanupSuccessful(files.Count);
  }
}