using System;
using StateWithImplicitContextPassing.Interfaces;
using StateWithImplicitContextPassing.OtherServices;

namespace StateWithImplicitContextPassing.States
{
  public class SwitchedOnState : LightSwitchState
  {
    private readonly LightSwitchStates _lightSwitchStates;
    private readonly Light _light;
    private readonly LightSwitchContext _context;

    public SwitchedOnState(LightSwitchStates lightSwitchStates, Light light, LightSwitchContext context)
    {
      _lightSwitchStates = lightSwitchStates;
      _light = light;
      _context = context;
    }

    public void SwitchOn()
    {
      Console.WriteLine("already switched on"); //what if I move to current state instead?
    }

    public void SwitchOff()
    {
      _context.MoveTo(_lightSwitchStates.SwitchedOff(_context));
    }

    public void OnEnter()
    {
      _light.PowerUp(); //should this be inside context or not?
      _context.RegisterSwitchOn();
    }
  }
}