using System;
using StateWithImplicitContextPassing.Interfaces;
using StateWithImplicitContextPassing.OtherServices;

namespace StateWithImplicitContextPassing.States
{ 
  public class SwitchedOffState : LightSwitchState
  {
    private readonly LightSwitchStates _lightSwitchStates;
    private readonly Light _light;
    private readonly LightSwitchContext _context;

    public SwitchedOffState(LightSwitchStates lightSwitchStates, Light light, LightSwitchContext context)
    {
      _lightSwitchStates = lightSwitchStates;
      _light = light;
      _context = context;
    }

    public void SwitchOn()
    {
      _context.MoveTo(_lightSwitchStates.SwitchedOn(_context));
    }

    public void SwitchOff()
    {
      Console.WriteLine("It's already dark");
      //TODO context.MoveTo(_lightSwitchStates.SwitchedOff()); //transition to self. What happens?
    }

    public void OnEnter()
    {
      _light.PowerDown();
    }
  }
}