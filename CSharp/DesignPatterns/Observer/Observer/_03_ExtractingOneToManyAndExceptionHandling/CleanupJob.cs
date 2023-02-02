using Observer.Common;

namespace Observer._03_ExtractingOneToManyAndExceptionHandling;

internal class CleanupJob
{
  private readonly ICleanedUpDir _cleanedUpDir;
  private readonly ICleanUpProcedure _cleanupProcedure;
  private ICleanupObserver _cleanupObserver;

  public CleanupJob(
    ICleanedUpDir cleanedUpDir,
    ICleanUpProcedure cleanupProcedure)
  {
    _cleanedUpDir = cleanedUpDir;
    _cleanupProcedure = cleanupProcedure;
    _cleanupObserver = new NullObserver();
  }

  public void Set(ICleanupObserver observer)
  {
    _cleanupObserver = observer;
  }

  public void Run()
  {
    var files = _cleanedUpDir.GetFilesToCleanup();
    _cleanupProcedure.RunOn(files);
    _cleanupObserver.OnCleanupSuccessful(files.Count);
  }
}