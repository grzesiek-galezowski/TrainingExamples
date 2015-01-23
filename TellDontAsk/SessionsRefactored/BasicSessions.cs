using System.Collections.Generic;

namespace SessionsRefactored
{
  public interface Sessions
  {
    void Add(Session session);
    void DumpTo(DumpDestination destination);
  }

  public class BasicSessions : Sessions
  {
    readonly List<Session> _sessions = new List<Session>();
    public void Add(Session session)
    {
      _sessions.Add(session);
    }

    public void DumpTo(DumpDestination destination)
    {
      foreach (var session in _sessions)
      {
        session.DumpTo(destination);
      }
    }
  }
}

