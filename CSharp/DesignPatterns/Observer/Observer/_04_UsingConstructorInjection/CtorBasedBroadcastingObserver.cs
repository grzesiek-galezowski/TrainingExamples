using System.Collections.Immutable;
using Observer.Common;

namespace Observer._04_UsingConstructorInjection;

internal class CtorBasedBroadcastingObserver : ICleanupObserver
{
  private readonly ISupport _support;
  private readonly IImmutableList<ICleanupObserver> _observers;

  public CtorBasedBroadcastingObserver(ISupport support, params ICleanupObserver[] observers)
  {
    _support = support;
    _observers = observers.ToImmutableArray();
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