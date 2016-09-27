using State.OtherServices;

namespace State
{
  public class MainObject
  {
    //states are stateless - all fields read only
    //state machine should be protected from threaded access
    //state machine should be thin - delegate all functionality (how much should be delegated to context?)
    //if multithreaded, SM should be tell don't ask
    //different ways of passing context
    // - through setter
    // - through getInitialState(this) and then each state passes context to another's factory method
    // - through parameters
    //two interfaces - signals and context
    //returning state instead of setting on context - consequences - always have to return state


    public static void Main(string[] args)
    {
      var consoleOutput = new ConsoleOutput();
      var light = new DiningRoomLight();
      var lightSwitchStatesFactory = new LightSwitchStatesFactory(light);
      var lightSwitchStateMachine = new LightSwitchStateMachine(
        lightSwitchStatesFactory.SwitchedOff());
      lightSwitchStateMachine.SwitchOn();
      lightSwitchStateMachine.SwitchOff();
      lightSwitchStateMachine.SwitchOn();
      lightSwitchStateMachine.SwitchOff();
      lightSwitchStateMachine.SwitchOff();
      lightSwitchStateMachine.SwitchOn();
      lightSwitchStateMachine.SwitchOn();
      lightSwitchStateMachine.SwitchOff();
      lightSwitchStateMachine.ShowSwitchesCountOn(consoleOutput);
    }
  }
}
