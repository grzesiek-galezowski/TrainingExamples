using System;

namespace FunctionalState
{
  public class LightSwitchStateMachine
  {
    public LightSwitchState CurrentState { get; }
    public int NumSwitches { get; } = 0;
    public Func<object> PowerUpLight { get; private set; }
    public Func<object> PowerDownLight { get; private set; }

    public LightSwitchStateMachine(
      LightSwitchState initialState, 
      int numSwitches, 
      Func<object> powerUpLight, 
      Func<object> powerDownLight)
    {
      CurrentState = initialState;
      NumSwitches = numSwitches;
      PowerUpLight = powerUpLight;
      PowerDownLight = powerDownLight;
    }

    public static LightSwitchStateMachine DispatchSignalTo(
      LightSwitchStateMachine machine,
      Func<LightSwitchStateMachine, LightSwitchStateMachine> whenSwitchedOn, 
      Func<LightSwitchStateMachine, LightSwitchStateMachine> whenSwitchedOff)
    {
      switch (machine.CurrentState)
      {
        case LightSwitchState.SwitchedOn:
          return whenSwitchedOn(machine);
        case LightSwitchState.SwitchedOff:
          return whenSwitchedOff(machine);
        default:
          throw new ArgumentOutOfRangeException();
      }
    }
  }
}