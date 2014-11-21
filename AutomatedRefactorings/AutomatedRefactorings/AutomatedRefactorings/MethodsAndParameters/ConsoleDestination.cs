using System;

namespace AutomatedRefactorings.MethodsAndParameters
{
  internal class ConsoleDestination : IMessageDestination
  {
    public void Send(string s)
    {
      Console.WriteLine(s);
    }
  }
}