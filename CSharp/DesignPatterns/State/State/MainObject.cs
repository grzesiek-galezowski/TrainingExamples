using System;
using State.Interfaces;
using State.OtherServices;
using State.Synchronization;

namespace State
{
  public class MainObject
  {
    //states are stateless - all fields read only - all state delegated to context or shared external services
    //state machine should be thin - delegate all functionality (how much should be delegated to context?)
    //state machine should be protected from threaded access
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
      var lightSwitch = CreateLightSwitchStateMachine();
      lightSwitch.SwitchOn();
      lightSwitch.SwitchOff();
      lightSwitch.SwitchOn();
      lightSwitch.SwitchOff();
      lightSwitch.SwitchOff();
      lightSwitch.SwitchOn();
      lightSwitch.SwitchOn();
      lightSwitch.SwitchOff();
      lightSwitch.ShowSwitchesCountOn(consoleOutput);
    }

    private static ILightSwitch CreateLightSwitchStateMachine()
    {
      var statesFactory = new LightSwitchStatesFactory(new DiningRoomLight());
      return new LightSwitchStateMachine(statesFactory.SwitchedOff());
    }
  }
}
