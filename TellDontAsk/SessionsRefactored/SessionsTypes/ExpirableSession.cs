using System;

namespace SessionsRefactored.SessionsTypes
{
  public class ExpirableSession : Session
  {
    private readonly Session _session;
    private readonly DateTime _expiryTime;

    public ExpirableSession(Session session, DateTime expiryTime)
    {
      _session = session;
      _expiryTime = expiryTime;
    }

    public void DumpTo(DumpDestination destination)
    {
      if (DateTime.Now < _expiryTime)
      {
        _session.DumpTo(destination);
      }
    }

    public void DoSomething()
    {
      _session.DoSomething();
    }
  }
}