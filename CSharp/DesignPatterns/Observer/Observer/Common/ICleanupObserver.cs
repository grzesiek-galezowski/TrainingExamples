namespace Observer.Common;

public interface ICleanupObserver
{
  void OnCleanupSuccessful(int filesCount);
}