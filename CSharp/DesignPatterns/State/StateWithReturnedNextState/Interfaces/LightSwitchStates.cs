namespace StateWithReturnedNextState.Interfaces
{
  public interface LightSwitchStates
  {
    LightSwitchState SwitchedOff();
    LightSwitchState SwitchedOn();
  }
}