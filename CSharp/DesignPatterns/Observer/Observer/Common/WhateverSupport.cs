namespace Observer.Common;

internal class WhateverSupport : ISupport
{
  public void NotifyingObserverFailed(Exception exception, Type observerType, int filesCount)
  {
    throw new NotImplementedException();
  }
}