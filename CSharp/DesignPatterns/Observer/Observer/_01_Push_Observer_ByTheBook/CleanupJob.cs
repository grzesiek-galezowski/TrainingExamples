using Observer.Common;

namespace Observer._01_Push_Observer_ByTheBook;

internal class CleanupJob
{
  private readonly ICleanedUpDir _cleanedUpDir;
  private readonly ICleanUpProcedure _cleanupProcedure;
  private readonly ISupport _support;
  private readonly IList<ICleanupObserver> _observers = new List<ICleanupObserver>();

  public CleanupJob(
    ICleanedUpDir cleanedUpDir,
    ICleanUpProcedure cleanupProcedure,
    ISupport support)
  {
    _cleanedUpDir = cleanedUpDir;
    _cleanupProcedure = cleanupProcedure;
    _support = support;
  }

  public void Attach(ICleanupObserver observer)
  {
    _observers.Add(observer);
  }
  
  //todo remove
  public void Detach(ICleanupObserver observer)
  {
    _observers.Remove(observer);
  }
  
  public void Run()
  {
    var files = _cleanedUpDir.GetFilesToCleanup();
    _cleanupProcedure.RunOn(files);
    NotifyObserversAboutCleanedUp(files);
  }

  private void NotifyObserversAboutCleanedUp(IReadOnlyCollection<ICleanedUpFile> files)
  {
    foreach (var observer in _observers)
    {
      try
      {
        observer.OnCleanupSuccessful(files.Count);
      }
      catch (Exception e)
      {
        _support.NotifyingObserverFailed(e, observer.GetType(), files.Count);
      }
    }
  }
}