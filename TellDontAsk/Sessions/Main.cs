using System;
using System.Collections.Generic;
using System.IO;

namespace Sessions
{
  public class Main
  {
    public void Program()
    {
      var sessions = new Sessions();
      AddExemplaryDataTo(sessions);

      //1
      foreach (var session in sessions.GetAll())
      {
        Console.WriteLine("==> BEGIN SESSION");
        Console.WriteLine(session.Owner);
        Console.WriteLine(session.Target);
        Console.WriteLine(session.Duration);
        Console.WriteLine("==> END SESSION");
      }

      //2
      using (var writer = File.CreateText("lolek.txt"))
      {
        foreach (var session1 in sessions.GetAll())
        {
          writer.WriteLine("==> BEGIN SESSION");
          writer.WriteLine(session1.Owner);
          writer.WriteLine(session1.Target);
          writer.WriteLine(session1.Duration);
          writer.WriteLine("==> END SESSION");
        }
      }

      //3
      var frames = new List<SessionInformationMessage>();
      foreach (var session2 in sessions.GetAll())
      {
        var frame = new SessionInformationMessage();
        frame.Owner = session2.Owner;
        frame.Target = session2.Target;
        frame.Duration = session2.Duration;
        frames.Add(frame);
      }
    }

    private static void AddExemplaryDataTo(Sessions sessions)
    {
      sessions.Add(new Session()
      {
        Duration = TimeSpan.FromDays(12),
        Owner = "Zenek",
        Target = "Astro device"
      });
      sessions.Add(new Session()
      {
        Duration = TimeSpan.FromDays(11),
        Owner = "Janek",
        Target = "Dimetra device"
      });
      sessions.Add(new Session()
      {
        Duration = TimeSpan.FromDays(1),
        Owner = "Czesiek",
        Target = "LTE device"
      });
    }
  }
}