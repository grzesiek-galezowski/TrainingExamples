using System;
using State.Interfaces;
using State.OtherServices;

namespace State.States
{ 
  public class SwitchedOffState : ILightSwitchState
  {
    private readonly ILightSwitchStates _lightSwitchStates;
    private readonly Light _light;

    public SwitchedOffState(ILightSwitchStates lightSwitchStates, Light light)
    {
      _lightSwitchStates = lightSwitchStates;
      _light = light;
    }

    public void SwitchOn(ILightSwitchContext context)
    {
      context.MoveTo(_lightSwitchStates.SwitchedOn());
    }

    public void SwitchOff(ILightSwitchContext context)
    {
      Console.WriteLine("It's already dark");
      //TODO context.MoveTo(_lightSwitchStates.SwitchedOff()); //transition to self. What happens?
    }

    public void OnEnter(ILightSwitchContext context)
    {
      _light.PowerDown();
    }
  }
}