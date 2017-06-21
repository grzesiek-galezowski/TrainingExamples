using StateWithImplicitContextPassing.Interfaces;
using StateWithImplicitContextPassing.OtherServices;
using StateWithImplicitContextPassing.States;

namespace StateWithImplicitContextPassing
{
  public class LightSwitchStatesFactory : LightSwitchStates
  {
    private readonly Light _light;

    public LightSwitchStatesFactory(Light light)
    {
      _light = light;
    }

    public LightSwitchState SwitchedOff(LightSwitchContext context)
    {
      return new SwitchedOffState(this, _light, context);
    }

    public LightSwitchState SwitchedOn(LightSwitchContext context)
    {
      return new SwitchedOnState(this, _light, context);
    }

    public LightSwitchState Initial(LightSwitchContext context)
    {
      return SwitchedOff(context);
    }
  }
}