using System;

namespace AutomatedRefactorings.MethodsAndParameters
{
  internal class ConsoleDestination : MessageDestination
  {
    public void Send(string s)
    {
      Console.WriteLine(s);
    }
  }
}