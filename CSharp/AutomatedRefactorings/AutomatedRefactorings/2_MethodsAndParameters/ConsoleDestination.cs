using System;

namespace AutomatedRefactorings._2_MethodsAndParameters
{
  internal class ConsoleDestination : MessageDestination
  {
    public void Send(string s)
    {
      Console.WriteLine(s);
    }
  }
}