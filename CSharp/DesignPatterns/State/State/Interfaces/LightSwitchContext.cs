namespace State.Interfaces
{
  public interface ILightSwitchContext
  {
    void MoveTo(ILightSwitchState nextState);
    void RegisterSwitchOn();
  }
}