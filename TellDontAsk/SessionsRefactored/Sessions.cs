using System.Collections.Generic;

namespace SessionsRefactored
{

  public class Sessions
  {
    readonly List<Session> _sessions = new List<Session>();
    public void Add(Session session)
    {
      _sessions.Add(session);
    }

    public IEnumerable<Session> GetAll()
    {
      return _sessions;
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

