namespace SessionsRefactored
{
  public interface Sessions
  {
    void Add(Session session);
    void DumpTo(DumpDestination destination);
  }
}