namespace SessionsRefactored
{
  public interface Session
  {
    void DumpTo(DumpDestination destination);
    void DoSomething(); //this is just any other logic
  }
}