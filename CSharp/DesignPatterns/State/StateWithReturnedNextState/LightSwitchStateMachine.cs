using StateWithReturnedNextState.Interfaces;
using StateWithReturnedNextState.OtherServices;

namespace StateWithReturnedNextState
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
      var nextState = _currentState.SwitchOn();
      MoveTo(nextState);
    }

    public void SwitchOff()
    {
      var nextState = _currentState.SwitchOff();
      MoveTo(nextState);
    }

    public void MoveTo(LightSwitchState nextState)
    {
      if (nextState.GetType() != _currentState.GetType()) //TODO explain this!
      {
        _currentState = nextState;
        _currentState.OnEnter(this); //what about OnExit()?
      }
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