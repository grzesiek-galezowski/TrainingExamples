using System;

namespace StateWithImplicitContextPassing.OtherServices
{
  public class ConsoleOutput : Output
  {
    public void Show(int numSwitches)
    {
      Console.WriteLine("Switches count " + numSwitches);
    }
  }
}