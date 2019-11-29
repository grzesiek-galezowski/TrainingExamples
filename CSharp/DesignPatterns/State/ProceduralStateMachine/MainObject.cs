using System;

namespace ProceduralStateMachine
{
  public class MainObject
  {
    public static void Main(string[] args)
    {
      var lightSwitch = new LightSwitchStateMachine();

      Console.WriteLine("=== Turn on 1");
      lightSwitch.TurnOn();
      Console.WriteLine("=== Turn on 2");
      lightSwitch.TurnOn();
      Console.WriteLine("=== Turn off 1");
      lightSwitch.TurnOff();
      Console.WriteLine("=== Turn off 2");
      lightSwitch.TurnOff();
    }
  }

}
