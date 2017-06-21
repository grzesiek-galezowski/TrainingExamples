namespace StateWithReturnedNextState.Interfaces
{
  public interface LightSwitchState
  {
    LightSwitchState SwitchOn(); //difference - no context required for now
    LightSwitchState SwitchOff();
    void OnEnter(LightSwitchContext context);
  }
}