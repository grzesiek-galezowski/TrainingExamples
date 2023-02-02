using Observer.Common;

namespace Observer._04_UsingConstructorInjection;

internal class CleanupJob
{
  private readonly ICleanedUpDir _cleanedUpDir;
  private readonly ICleanUpProcedure _cleanupProcedure;
  private readonly ICleanupObserver _cleanupObserver;

  public CleanupJob(
    ICleanedUpDir cleanedUpDir,
    ICleanUpProcedure cleanupProcedure, 
    ICleanupObserver cleanupObserver)
  {
    _cleanedUpDir = cleanedUpDir;
    _cleanupProcedure = cleanupProcedure;
    _cleanupObserver = cleanupObserver;
  }

  public void Run()
  {
    var files = _cleanedUpDir.GetFilesToCleanup();
    _cleanupProcedure.RunOn(files);
    _cleanupObserver.OnCleanupSuccessful(files.Count);
  }
}