using State.Interfaces;
using State.OtherServices;

namespace State
{
  public class LightSwitchStateMachine : ILightSwitch, ILightSwitchContext
  {
    private ILightSwitchState _currentState;
    private int _numSwitches = 0;

    public LightSwitchStateMachine(ILightSwitchState initialState)
    {
      MoveTo(initialState); //has to be the last line!!! Alternative - Start() method
    }

    public void SwitchOn()
    {
      _currentState.SwitchOn(this);
    }

    public void SwitchOff()
    {
      _currentState.SwitchOff(this);
    }

    public void MoveTo(ILightSwitchState nextState)
    {
      _currentState = nextState;
      _currentState.OnEnter(this); //what about OnExit()?
    }

    public void RegisterSwitchOn()
    {
      _numSwitches++;
    }

    public void ShowSwitchesCountOn(Output output)
    {
      output.Show(_numSwitches); //does not require state!
    }
  }
}