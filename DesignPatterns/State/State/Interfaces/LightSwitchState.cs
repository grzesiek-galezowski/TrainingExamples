namespace State.Interfaces
{
  public interface LightSwitchState
  {
    void SwitchOn(LightSwitchContext context);
    void SwitchOff(LightSwitchContext context);
    void OnEnter(LightSwitchContext context);
  }
}