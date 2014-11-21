using System;

namespace EventAnomalies.Example2
{
  public class ConsoleErrorService : IErrorService
  {
    public void LogError(string message)
    {
      Console.WriteLine(message);
    }
  }
}