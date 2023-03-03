namespace UnitTestRunnerPackageExercise;

public class FlatFileDestination : ITestResultsDestination
{
  public void Send(string entries)
  {
    File.AppendAllText(Path.GetTempFileName(), entries);
  }
}