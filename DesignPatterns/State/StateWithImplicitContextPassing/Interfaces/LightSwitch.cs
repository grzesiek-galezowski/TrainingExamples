using StateWithImplicitContextPassing.OtherServices;

namespace StateWithImplicitContextPassing.Interfaces
{
  public interface LightSwitch
  {
    void SwitchOn();
    void SwitchOff();
    void ShowSwitchesCountOn(Output output);
  }
}