using StateWithReturnedNextState.Interfaces;
using StateWithReturnedNextState.OtherServices;
using StateWithReturnedNextState.States;

namespace StateWithReturnedNextState
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