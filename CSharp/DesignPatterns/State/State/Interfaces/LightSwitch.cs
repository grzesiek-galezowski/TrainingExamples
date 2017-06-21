using State.OtherServices;

namespace State.Interfaces
{
  public interface LightSwitch
  {
    void SwitchOn();
    void SwitchOff();
    void ShowSwitchesCountOn(Output output);
  }
}