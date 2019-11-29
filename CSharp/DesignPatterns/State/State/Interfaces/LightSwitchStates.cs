namespace State.Interfaces
{
  public interface ILightSwitchStates
  {
    ILightSwitchState SwitchedOff();
    ILightSwitchState SwitchedOn();
  }
}