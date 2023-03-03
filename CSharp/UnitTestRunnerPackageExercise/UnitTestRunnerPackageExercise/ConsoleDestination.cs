namespace UnitTestRunnerPackageExercise;

public class ConsoleDestination : ITestResultsDestination
{
  public void Send(string entries)
  {
    Console.WriteLine(entries);
  }
}