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

    public static LightSwitchStateMachine Handle(
      LightSwitchStateMachine machine,
      Func<LightSwitchStateMachine, LightSwitchStateMachine> inSwitchedOnHandling, 
      Func<LightSwitchStateMachine, LightSwitchStateMachine> inSwitchedOffHandling)
    {
      switch (machine.CurrentState)
      {
        case LightSwitchState.SwitchedOn:
          return inSwitchedOnHandling(machine);
        case LightSwitchState.SwitchedOff:
          return inSwitchedOffHandling(machine);
        default:
          throw new ArgumentOutOfRangeException();
      }
    }
  }
}