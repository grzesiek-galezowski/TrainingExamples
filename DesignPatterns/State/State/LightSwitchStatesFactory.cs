using State.Interfaces;
using State.OtherServices;
using State.States;

namespace State
{
  public class LightSwitchStatesFactory : LightSwitchStates
  {
    private readonly Light _light;

    public LightSwitchStatesFactory(Light light)
    {
      _light = light;
    }

    public LightSwitchState SwitchedOff()
    {
      return new SwitchedOffState(this, _light);
    }

    public LightSwitchState SwitchedOn()
    {
      return new SwitchedOnState(this, _light);
    }
  }
}