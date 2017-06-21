using System;

namespace SessionsRefactored.SessionsTypes
{
  public class BasicSession : Session
  {
    private readonly SessionData _sessionData;

    public BasicSession(SessionData sessionData)
    {
      _sessionData = sessionData;
    }

    public void DumpTo(DumpDestination destination)
    {
      destination.BeginNewSessionDump();
      destination.AddId(_sessionData.Id);
      destination.AddOwner(_sessionData.Owner);
      destination.AddTarget(_sessionData.Target);
      destination.EndCurrentSessionDump();
    }

    public void DoSomething()
    {
      Console.WriteLine("I am doing something, lol!");
    }
  }
}