using System;
using State.Interfaces;
using State.OtherServices;

namespace State.States
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

    public void SwitchOn(LightSwitchContext context)
    {
      context.MoveTo(_lightSwitchStates.SwitchedOn());
    }

    public void SwitchOff(LightSwitchContext context)
    {
      Console.WriteLine("It's already dark");
      //TODO context.MoveTo(_lightSwitchStates.SwitchedOff()); //transition to self. What happens?
    }

    public void OnEnter(LightSwitchContext context)
    {
      _light.PowerDown();
    }
  }
}