using System;
using FunctionalState.Interfaces;
using FunctionalState.OtherServices;
using static FunctionalState.LightSwitchStateMachine;

namespace FunctionalState
{
  public class MainObject
  {

    public static void Main(string[] args)
    {

      var consoleOutput = new ConsoleOutput();
      var light = new DiningRoomLight();
      var powerUpLight = new Func<object>(() => PowerUpLight(light));
      var powerDownLight = new Func<object>(() => PowerDownLight(light));
      var machine = new LightSwitchStateMachine(LightSwitchState.SwitchedOff, 0, powerUpLight, powerDownLight);

      machine = Handle(machine,
        SwitchedOnSignal.HandleInSwitchedOnState,
        SwitchedOnSignal.HandleInSwitchedOffState
      );

      machine = Handle(machine,
        SwitchedOffSignal.HandleInSwitchedOnState,
        SwitchedOffSignal.HandleInSwitchedOffState
      );

      machine = Handle(machine,
        SwitchedOffSignal.HandleInSwitchedOnState,
        SwitchedOffSignal.HandleInSwitchedOffState
      );

      var whatever = ShowOutput(consoleOutput, machine.NumSwitches);
    }

    private static object PowerDownLight(Light light)
    {
      light.PowerDown();
      return null;
    }

    private static object PowerUpLight(Light light)
    {
      light.PowerUp();
      return null;
    }

    private static object ShowOutput(ConsoleOutput consoleOutput, int num)
    {
      consoleOutput.Show(num);
      return null;
    }
  }

  public class SwitchedOffSignal
  {
    public static LightSwitchStateMachine HandleInSwitchedOnState(LightSwitchStateMachine arg)
    {
      var result = arg.PowerDownLight(); //impure
      return new LightSwitchStateMachine(LightSwitchState.SwitchedOff, 
        arg.NumSwitches,
        arg.PowerUpLight,
        arg.PowerDownLight);
    }

    public static LightSwitchStateMachine HandleInSwitchedOffState(LightSwitchStateMachine arg)
    {
      return arg;
    }
  }

  public static class SwitchedOnSignal
  {
    public static LightSwitchStateMachine HandleInSwitchedOnState(LightSwitchStateMachine arg)
    {
      return arg;
    }

    public static LightSwitchStateMachine HandleInSwitchedOffState(LightSwitchStateMachine arg)
    {
      var result = arg.PowerUpLight(); //impure
      return new LightSwitchStateMachine(LightSwitchState.SwitchedOn, 
        arg.NumSwitches+1, 
        arg.PowerUpLight, 
        arg.PowerDownLight);
    }
  }
}
