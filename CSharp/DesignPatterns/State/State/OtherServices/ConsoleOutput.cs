using System;

namespace State.OtherServices
{
  public class ConsoleOutput : Output
  {
    public void Show(int numSwitches)
    {
      Console.WriteLine("Switches count " + numSwitches);
    }
  }
}