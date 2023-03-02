namespace UnitTestRunnerPackageExercise;

public class FlatFileDestination
{
  public void Send(List<string> entries)
  {
    File.AppendAllLines(Path.GetTempFileName(), entries);
  }
}