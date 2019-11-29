namespace State.Interfaces
{
  public interface ILightSwitchState
  {
    void SwitchOn(ILightSwitchContext context);
    void SwitchOff(ILightSwitchContext context);
    void OnEnter(ILightSwitchContext context);
  }
}