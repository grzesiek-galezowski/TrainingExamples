namespace Observer.Common;

internal class ConcreteObserver : ICleanupObserver
{
  public void OnCleanupSuccessful(int filesCount)
  {
    throw new NotImplementedException();
  }
}