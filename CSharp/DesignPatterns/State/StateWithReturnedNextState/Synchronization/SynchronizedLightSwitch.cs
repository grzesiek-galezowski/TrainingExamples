using StateWithReturnedNextState.Interfaces;
using StateWithReturnedNextState.OtherServices;

namespace StateWithReturnedNextState.Synchronization
{
  public class SynchronizedLightSwitch : LightSwitch
  {
    private readonly LightSwitch _lightSwitch;
    private readonly object _syncRoot = new object();

    public SynchronizedLightSwitch(LightSwitch lightSwitch)
    {
      _lightSwitch = lightSwitch;
    }

    public void SwitchOn()
    {
      lock (_syncRoot)
      {
        _lightSwitch.SwitchOn();
      }
    }

    public void SwitchOff()
    {
      lock (_syncRoot)
      {
        _lightSwitch.SwitchOff();
      }
    }

    public void ShowSwitchesCountOn(Output output)
    {
      lock (_syncRoot)
      {
        _lightSwitch.ShowSwitchesCountOn(output);
      }
    }
  }
}