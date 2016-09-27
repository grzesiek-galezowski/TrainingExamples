using System;
using State.Interfaces;
using State.OtherServices;

namespace State.States
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

    public void SwitchOn(LightSwitchContext context)
    {
      Console.WriteLine("already switched on"); //what if I move to current state instead?
    }

    public void SwitchOff(LightSwitchContext context)
    {
      context.MoveTo(_lightSwitchStates.SwitchedOff());
    }

    public void OnEnter(LightSwitchContext context)
    {
      _light.PowerUp(); //should this be inside context or not?
      context.RegisterSwitchOn();
    }
  }
}