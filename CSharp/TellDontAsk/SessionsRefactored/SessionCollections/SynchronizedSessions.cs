using System;

namespace SessionsRefactored
{
  public class SynchronizedSessions : Sessions
  {
    private readonly Sessions _sessions;
    readonly object _syncRoot = new object();

    public SynchronizedSessions(Sessions sessions)
    {
      _sessions = sessions;
    }

    public void Add(Session session)
    {
      lock (_syncRoot)
      {
        _sessions.Add(session);
      }
    }

    public void DumpTo(DumpDestination destination)
    {
      lock (_syncRoot)
      {
        _sessions.DumpTo(destination);
      }
    }
  }
}