using StateWithReturnedNextState.OtherServices;

namespace StateWithReturnedNextState.Interfaces
{
  public interface LightSwitch
  {
    void SwitchOn();
    void SwitchOff();
    void ShowSwitchesCountOn(Output output);
  }
}