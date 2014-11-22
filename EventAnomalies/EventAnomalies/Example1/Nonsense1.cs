using System;

namespace EventAnomalies.Example1
{
    public class Nonsense1
    {
      private event ErrorHandling SomethingHappened;

      public Nonsense1()
      {
        this.SomethingHappened += this.LogError;
      }

      public void Process(int argument)
      {
        if (argument != 1)
        {
          this.SomethingHappened("someone is trying to cheat on me!");
        }
      }

      public void LogError(string message)
      {
        Console.WriteLine(message);
      }
    }
}