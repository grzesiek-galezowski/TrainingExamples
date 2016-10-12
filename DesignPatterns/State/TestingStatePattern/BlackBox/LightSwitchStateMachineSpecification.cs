using NSubstitute;
using NUnit.Framework;
using State;
using State.Interfaces;
using State.OtherServices;

namespace TestingStatePattern.BlackBox
{
  class LightSwitchStateMachineSpecification
  {
    [Test]
    public void ShouldPowerDownTheLightWhenSwitchedOffInSwitchedOffState()
    {
      //GIVEN
      var light = Substitute.For<Light>();
      var lightSwitchStateMachine = CreateSwitchedOffStateMachine(light);

      light.Received(1).PowerDown();
    }

    [Test]
    public void ShouldNotRegisterAnySwitchesWhenSwitchedOffInSwitchedOffState()
    {
      //GIVEN
      var light = Substitute.For<Light>();
      var lightSwitchStateMachine = CreateSwitchedOffStateMachine(light);

      AssertSwitchesCount(0, lightSwitchStateMachine);
    }

    [Test]
    public void ShouldPowerUpTheLightWhenSwitchedOnInSwitchedOffState()
    {
      //GIVEN
      var light = Substitute.For<Light>();
      var lightSwitchStateMachine = CreateSwitchedOffStateMachine(light);

      //WHEN
      lightSwitchStateMachine.SwitchOn();

      //THEN
      light.Received(1).PowerUp();
      AssertSwitchesCount(1, lightSwitchStateMachine);
    }
    [Test]
    public void ShouldRegisterASingleSwitchWhenSwitchedOnInSwitchedOffState()
    {
      //GIVEN
      var light = Substitute.For<Light>();
      var lightSwitchStateMachine = CreateSwitchedOffStateMachine(light);

      //WHEN
      lightSwitchStateMachine.SwitchOn();

      //THEN
      AssertSwitchesCount(1, lightSwitchStateMachine);
    }

    [Test]
    public void ShouldPowerDownTheLightWhenSwitchedOffWhileInSwitchedOnState()
    {
      //GIVEN
      var light = Substitute.For<Light>();
      var lightSwitchStateMachine = CreateSwitchedOnStateMachine(light);
      

      lightSwitchStateMachine.SwitchOff();

      //THEN
      light.Received(1).PowerDown();
    }

    private static LightSwitchStateMachine CreateSwitchedOnStateMachine(Light light)
    {
      var lightSwitchStateMachine = CreateSwitchedOffStateMachine(light);

      //WHEN
      lightSwitchStateMachine.SwitchOn();
      light.ClearReceivedCalls();
      return lightSwitchStateMachine;
    }

    private static void AssertSwitchesCount(int numSwitches, LightSwitch lightSwitchStateMachine)
    {
      var output = Substitute.For<Output>();
      lightSwitchStateMachine.ShowSwitchesCountOn(output);
      output.Received(1).Show(numSwitches);
    }

    private static LightSwitchStateMachine CreateSwitchedOffStateMachine(Light light)
    {
      var switchedOffStateMachine = new LightSwitchStateMachine(new LightSwitchStatesFactory(light).SwitchedOff());
      return switchedOffStateMachine;
    }
  }
}
