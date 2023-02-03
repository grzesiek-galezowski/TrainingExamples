using System.Collections.Immutable;

namespace Observer.Common;

internal class ParallelBroadcastingObserver : ICleanupObserver
{
  private readonly ISupport _support;
  private ImmutableList<ICleanupObserver> _observers = ImmutableList<ICleanupObserver>.Empty;

  public ParallelBroadcastingObserver(ISupport support)
  {
    _support = support;
  }

  public void Attach(ICleanupObserver observer)
  {
    _observers = _observers.Add(observer);
  }

  public void OnCleanupSuccessful(int filesCount)
  {
    //if we wanted to use async, we'd have to change the signatures to use tasks
    Parallel.ForEach(_observers, observer =>
    {
      try
      {
        observer.OnCleanupSuccessful(filesCount);
      }
      catch (Exception e)
      {
        _support.NotifyingObserverFailed(e, observer.GetType(), filesCount);
      }
    });
  }

}