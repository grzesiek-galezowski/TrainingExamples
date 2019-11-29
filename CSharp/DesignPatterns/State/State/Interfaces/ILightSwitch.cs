using State.OtherServices;

namespace State.Interfaces
{
  public interface ILightSwitch
  {
    void SwitchOn();
    void SwitchOff();
    void ShowSwitchesCountOn(Output output);
  }
}