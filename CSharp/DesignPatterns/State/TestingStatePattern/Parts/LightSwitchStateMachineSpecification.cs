using NSubstitute;
using NUnit.Framework;
using State;
using State.Interfaces;

namespace TestingStatePattern.Parts
{
  public class LightSwitchStateMachineSpecification
  {
    [Test]
    public void ShouldEnterItsInitialStateOnCreation()
    {
      //GIVEN
      var initialState = Substitute.For<ILightSwitchState>();
      ;

      //WHEN
      var machine = new LightSwitchStateMachine(initialState);

      //THEN
      initialState.Received(1).OnEnter(machine);
    }

    [Test]
    public void ShouldEnterItsNextStateAndRouteSignalsToItAfterMovingToNextState()
    {
      //GIVEN
      var initialState = Substitute.For<ILightSwitchState>();
      ;
      var nextState = Substitute.For<ILightSwitchState>();
      ;
      var machine = new LightSwitchStateMachine(initialState);

      //WHEN
      machine.MoveTo(nextState);

      //THEN
      nextState.Received(1).OnEnter(machine);
      machine.SwitchOn();
      AssertMachineIsIn(nextState, machine); //no getters!!!
    }

    [Test]
    public void ShouldRouteSwitchOnRequestToCurrentState()
    {
      //GIVEN
      var initialState = Substitute.For<ILightSwitchState>();
      var machine = new LightSwitchStateMachine(initialState);

      //WHEN
      machine.SwitchOn();

      //THEN
      initialState.Received(1).SwitchOn(machine);
      AssertMachineIsIn(initialState, machine);
    }

    //etc. etc.

    private static void AssertMachineIsIn(ILightSwitchState nextState, ILightSwitchContext machine)
    {
      nextState.Received(1).SwitchOn(machine);
    }
  }
}
