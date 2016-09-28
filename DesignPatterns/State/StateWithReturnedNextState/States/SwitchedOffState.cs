using System;
using StateWithReturnedNextState.Interfaces;
using StateWithReturnedNextState.OtherServices;

namespace StateWithReturnedNextState.States
{ 
  public class SwitchedOffState : LightSwitchState
  {
    private readonly LightSwitchStates _lightSwitchStates;
    private readonly Light _light;

    public SwitchedOffState(LightSwitchStates lightSwitchStates, Light light)
    {
      _lightSwitchStates = lightSwitchStates;
      _light = light;
    }

    public LightSwitchState SwitchOn()
    {
      return _lightSwitchStates.SwitchedOn();
    }

    public LightSwitchState SwitchOff()
    {
      Console.WriteLine("It's already dark");
      return this;
    }

    public void OnEnter(LightSwitchContext context)
    {
      _light.PowerDown();
    }
  }
}