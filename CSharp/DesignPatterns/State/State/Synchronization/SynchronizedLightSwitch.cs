using State.Interfaces;
using State.OtherServices;

namespace State.Synchronization
{
  public class SynchronizedLightSwitch : ILightSwitch
  {
    private readonly ILightSwitch _lightSwitch;
    private readonly object _syncRoot = new object();

    public SynchronizedLightSwitch(ILightSwitch lightSwitch)
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