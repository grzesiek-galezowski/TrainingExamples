using NSubstitute;
using NUnit.Framework;
using TddEbook.TddToolkit.NSubstitute;
using unit_tests_csharp.P04NSubstitute.Ex04ProductionCode;

namespace unit_tests_csharp.P04NSubstitute
{
  public class Ex04NoMoreInteractions
  {

    [Test]
    public void ShouldTranslateShootingCommandToShootingReport()
    {
      //GIVEN
      var executionEngine = Substitute.For<IExecutionEngine>();
      var interpreter = new CommandInterpreter(executionEngine);

      //WHEN
      interpreter.OnEvent(CommandInterpreter.Shooting);

      //THEN
      XReceived.Only(() =>
      {
        executionEngine.ShootingStarted();
      });
    }

    [Test]
    public void ShouldTranslateRobberyCommandToRobberyReport()
    {
      //GIVEN
      var executionEngine = Substitute.For<IExecutionEngine>();
      var interpreter = new CommandInterpreter(executionEngine);

      //WHEN
      interpreter.OnEvent(CommandInterpreter.Robbery);

      //THEN
      XReceived.Only(() =>
      {
        executionEngine.RobberyTookPlace();
      });
    }

    [Test]
    public void ShouldTranslateIncidentCommandToIncidentReport()
    {
      //GIVEN
      var executionEngine = Substitute.For<IExecutionEngine>();
      var interpreter = new CommandInterpreter(executionEngine);

      //WHEN
      interpreter.OnEvent(CommandInterpreter.Incident);

      //THEN
      XReceived.Only(() =>
      {
        executionEngine.IncidentDiscovered();
      });
    }
  }
}