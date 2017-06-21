namespace SessionsRefactored.SessionsTypes
{
  public class HiddenSession : Session
  {
    private readonly Session _session;

    public HiddenSession(Session session)
    {
      _session = session;
    }

    public void DumpTo(DumpDestination destination)
    {
      //It's hidden, remember!!!
    }

    public void DoSomething()
    {
      //Even though it's hidden, it still has a job to do!
      _session.DoSomething();
    }
  }
}