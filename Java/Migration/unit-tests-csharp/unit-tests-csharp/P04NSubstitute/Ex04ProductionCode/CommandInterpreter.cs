namespace unit_tests_csharp.P04NSubstitute.Ex04ProductionCode
{
  public class CommandInterpreter
  {
    private readonly IExecutionEngine _executionEngine;
    public const string Shooting = "shooting";
    public const string Robbery = "robbery";
    public const string Incident = "incident";

    public CommandInterpreter(IExecutionEngine executionEngine)
    {
      _executionEngine = executionEngine;
    }

    public void OnEvent(string command)
    {
      switch (command)
      {
        case Shooting:
          _executionEngine.ShootingStarted();
          break;
        case Incident:
          _executionEngine.IncidentDiscovered();
          break;
        case Robbery:
          _executionEngine.RobberyTookPlace();
          break;
      }
    }
  }
}