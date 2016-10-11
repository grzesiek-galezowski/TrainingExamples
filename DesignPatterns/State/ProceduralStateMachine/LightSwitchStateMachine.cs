using System;

namespace FunctionalState
{
  public class LightSwitchStateMachine
  {
    public void HandleSignals(Signals[] signals)
    {
      foreach (var signal in signals)
      {
        
      switch (signal)
      {
        case Signals.SwitchOn:
          _
          break;
        case Signals.SwitchOff:
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof(signals), signals, null);
      }
      }
    }
  }

  public enum Signals
  {
    SwitchOn, SwitchOff
  }
}