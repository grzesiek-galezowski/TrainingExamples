using System;

namespace EventAnomalies.Example3
{
  public class ConsoleErrorService : IDisposable
  {
    private readonly Nonsense3 _nonsense;

    public ConsoleErrorService(Nonsense3 nonsense)
    {
      _nonsense = nonsense;
      _nonsense.SomethingHappened += this.LogError;
    }

    //!!!
    private void LogError(string message)
    {
      Console.WriteLine(message);
    }

    public void Dispose()
    {
      _nonsense.SomethingHappened -= this.LogError;
    }
  }
}