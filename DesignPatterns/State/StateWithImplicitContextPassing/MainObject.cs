using StateWithImplicitContextPassing.OtherServices;
using StateWithImplicitContextPassing.Synchronization;

namespace StateWithImplicitContextPassing
{
  public class MainObject
  {
    //implicit state passing - need to pass factory inside, no need for context in state methods


    public static void Main(string[] args)
    {
      var consoleOutput = new ConsoleOutput();
      var light = new DiningRoomLight();
      var lightSwitchStatesFactory = new LightSwitchStatesFactory(light);
      var lightSwitchStateMachine = 
        CreateLightSwitchStateMachine(lightSwitchStatesFactory);
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
      LightSwitchStatesFactory states)
    {
      return new SynchronizedLightSwitch(new LightSwitchStateMachine(
        states));
    }
  }
}
