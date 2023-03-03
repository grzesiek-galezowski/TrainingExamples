namespace UnitTestRunnerPackageExercise;

public class ConsoleResultsReport : ITestResultsReport
{
  private readonly List<string> _entries = new();
  private readonly FlatFileDestination _destination;

  public ConsoleResultsReport()
  {
    _destination = new FlatFileDestination();
  }

  public void TestPassed(FullyQualifiedTestName fullyQualifiedTestName)
  {
    _entries.Add($"Passed: {fullyQualifiedTestName}");
  }

  public void EndOfSuite(string suiteName)
  {
    _entries.Add("Ending suite " + suiteName);
  }

  public void TestFailed(FullyQualifiedTestName fullyQualifiedTestName, Exception failureRootCause)
  {
    _entries.Add($"Failed: {fullyQualifiedTestName}{Environment.NewLine}{failureRootCause}");
  }

  public void StartingTest(FullyQualifiedTestName fullyQualifiedTestName)
  {
    _entries.Add($"Starting: "+ $"{fullyQualifiedTestName}");
  }

  public void StartSuite(string suiteName)
  {
    _entries.Add("Starting suite " + suiteName);
  }

  public void ReportToUser()
  {
    _destination.Send(string.Join(Environment.NewLine, _entries));
  }
}

//bug different languages (e.g. Polish, English)
//bug different formats (e.g. JSON)