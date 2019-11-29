using System;
using State.Interfaces;
using State.OtherServices;

namespace State.States
{
  public class SwitchedOnState : ILightSwitchState
  {
    private readonly ILightSwitchStates _lightSwitchStates;
    private readonly Light _light;

    public SwitchedOnState(ILightSwitchStates lightSwitchStates, Light light)
    {
      _lightSwitchStates = lightSwitchStates;
      _light = light;
    }

    public void SwitchOn(ILightSwitchContext context)
    {
      Console.WriteLine("already switched on"); //what if I move to current state instead?
    }

    public void SwitchOff(ILightSwitchContext context)
    {
      context.MoveTo(_lightSwitchStates.SwitchedOff());
    }

    public void OnEnter(ILightSwitchContext context)
    {
      _light.PowerUp(); //should this be inside context or not?
      context.RegisterSwitchOn();
    }
  }
}