namespace UnitTestRunnerPackageExercise;

public interface ITestResultsDestination
{
  void Send(string entries);
}

public class FlatFileDestination : ITestResultsDestination
{
  public void Send(string entries)
  {
    File.AppendAllText(Path.GetTempFileName(), entries);
  }
}