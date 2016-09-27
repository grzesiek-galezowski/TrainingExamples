using State.Interfaces;
using State.OtherServices;

namespace State
{
  public class LightSwitchStateMachine : LightSwitch, LightSwitchContext
  {
    private LightSwitchState _currentState;
    private int _numSwitches = 0;

    public LightSwitchStateMachine(LightSwitchState initialState)
    {
      _currentState = initialState;
      _currentState.OnEnter(this); //has to be the last line!!! Alternative - Start() method
    }

    public void SwitchOn()
    {
      _currentState.SwitchOn(this);
    }

    public void SwitchOff()
    {
      _currentState.SwitchOff(this);
    }

    void LightSwitchContext.MoveTo(LightSwitchState nextState)
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