namespace Observer._01_Push_Observer_ByTheBook;

public interface ICleanupObserver
{
  void OnCleanupSuccessful(int filesCount);
}