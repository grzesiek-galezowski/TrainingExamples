using System;

namespace SessionsRefactored
{
  public class SynchronizedSessions : Sessions
  {
    public SynchronizedSessions(BasicSessions basicSessions)
    {
      throw new NotImplementedException();
    }

    public void Add(Session session)
    {
      throw new NotImplementedException();
    }

    public void DumpTo(DumpDestination destination)
    {
      throw new NotImplementedException();
    }
  }
}