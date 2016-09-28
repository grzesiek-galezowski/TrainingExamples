using System;
using StateWithReturnedNextState.Interfaces;
using StateWithReturnedNextState.OtherServices;

namespace StateWithReturnedNextState.States
{
  public class SwitchedOnState : LightSwitchState
  {
    private readonly LightSwitchStates _lightSwitchStates;
    private readonly Light _light;

    public SwitchedOnState(LightSwitchStates lightSwitchStates, Light light)
    {
      _lightSwitchStates = lightSwitchStates;
      _light = light;
    }

    public LightSwitchState SwitchOn()
    {
      Console.WriteLine("already switched on"); //what if I move to current state instead?
      return this;
    }

    public LightSwitchState SwitchOff()
    {
      return _lightSwitchStates.SwitchedOff();
    }

    public void OnEnter(LightSwitchContext context)
    {
      _light.PowerUp(); //should this be inside context or not?
      context.RegisterSwitchOn();
    }
  }
}