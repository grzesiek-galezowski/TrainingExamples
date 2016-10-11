using System;
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

  }
