using System;
using ProceduralStateMachine.OtherServices;

namespace ProceduralStateMachine
{
  public class LightSwitchStateMachine
  {
    private LightSwitchState _state;
    private DiningRoomLight _light;

    public LightSwitchStateMachine()
    {
      _state = LightSwitchState.Off;
      _light = new DiningRoomLight();
    }

    public void TurnOn()
    {
      if (_state == LightSwitchState.Off)
      {
        _light.PowerUp();
        _state = LightSwitchState.On; //!!
        Console.WriteLine("Light power up completed");
      }
      else if (_state == LightSwitchState.On)
      {
        Console.WriteLine("Beep, already turned on");
      }
    }

    public void TurnOff()
    {
      if (_state == LightSwitchState.Off)
      {
        Console.WriteLine("Beep, already turned off");
      }
      else if (_state == LightSwitchState.On)
      {
        _light.PowerDown();
        _state = LightSwitchState.Off; //!!
        Console.WriteLine("Light power down completed");
      }

    }
  }

  public enum Signals
  {
    SwitchOn, SwitchOff
  }
}