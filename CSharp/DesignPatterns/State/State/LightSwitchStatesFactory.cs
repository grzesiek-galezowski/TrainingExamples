using State.Interfaces;
using State.OtherServices;
using State.States;

namespace State
{
  public class LightSwitchStatesFactory : ILightSwitchStates
  {
    private readonly Light _light;

    public LightSwitchStatesFactory(Light light)
    {
      _light = light;
    }

    public ILightSwitchState SwitchedOff()
    {
      return new SwitchedOffState(this, _light);
    }

    public ILightSwitchState SwitchedOn()
    {
      return new SwitchedOnState(this, _light);
    }
  }
}