namespace UnitTestRunnerPackageExercise;

public interface ITestResultsDestination
{
  void Send(string entries);
}