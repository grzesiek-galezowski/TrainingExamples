namespace Observer.Common;

public interface ISupport
{
  void NotifyingObserverFailed(Exception exception, Type observerType, int filesCount);
}