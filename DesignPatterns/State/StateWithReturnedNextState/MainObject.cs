using StateWithReturnedNextState.Interfaces;
using StateWithReturnedNextState.OtherServices;
using StateWithReturnedNextState.Synchronization;

namespace StateWithReturnedNextState
{
  public class MainObject
  {

    //returning state instead of setting on context 
    //consequences:
    //- always have to return state
    //- MoveState needs to change whether transition actually occurs


    public static void Main(string[] args)
    {
      var consoleOutput = new ConsoleOutput();
      var light = new DiningRoomLight();
      var lightSwitchStatesFactory = new LightSwitchStatesFactory(light);
      var lightSwitchStateMachine = 
        CreateLightSwitchStateMachine(lightSwitchStatesFactory.SwitchedOff());
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

    private static SynchronizedLightSwitch CreateLightSwitchStateMachine(
      LightSwitchState lightSwitchState)
    {
      return new SynchronizedLightSwitch(new LightSwitchStateMachine(
        lightSwitchState));
    }
  }
}
