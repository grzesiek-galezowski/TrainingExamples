using StateWithImplicitContextPassing.Interfaces;
using StateWithImplicitContextPassing.OtherServices;

namespace StateWithImplicitContextPassing
{
  public class LightSwitchStateMachine : LightSwitch, LightSwitchContext
  {
    private LightSwitchState _currentState;
    private int _numSwitches = 0;

    public LightSwitchStateMachine(LightSwitchStates states)
    {
      //the factory must be passed inside!
      MoveTo(states.Initial(this)); //has to be the last line!!! Alternative - Start() method
    }

    public void SwitchOn()
    {
      _currentState.SwitchOn();
    }

    public void SwitchOff()
    {
      _currentState.SwitchOff();
    }

    public void MoveTo(LightSwitchState nextState)
    {
      _currentState = nextState;
      _currentState.OnEnter(); //what about OnExit()?
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