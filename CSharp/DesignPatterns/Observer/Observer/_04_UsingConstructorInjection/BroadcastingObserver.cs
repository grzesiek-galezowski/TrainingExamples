using Observer.Common;

namespace Observer._04_UsingConstructorInjection;

internal class BroadcastingObserver : ICleanupObserver
{
  private readonly ISupport _support;
  private readonly IList<ICleanupObserver> _observers = new List<ICleanupObserver>();

  public BroadcastingObserver(ISupport support)
  {
    _support = support;
  }

  public void Attach(ICleanupObserver observer)
  {
    _observers.Add(observer);
  }

  public void OnCleanupSuccessful(int filesCount)
  {
    foreach (var observer in _observers)
    {
      try
      {
        observer.OnCleanupSuccessful(filesCount);
      }
      catch (Exception e)
      {
        _support.NotifyingObserverFailed(e, observer.GetType(), filesCount);
      }
    }
  }

}