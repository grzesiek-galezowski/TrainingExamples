namespace UnitTestRunnerPackageExercise;

public class ConsoleResultsReport : ITestResultsReport
{
  private readonly List<string> _entries = new();
  private readonly FlatFileDestination _destination;

  public ConsoleResultsReport()
  {
    _destination = new FlatFileDestination();
  }

  public void Passed(FullyQualifiedTestName fullyQualifiedTestName)
  {
    _entries.Add($"Passed: {fullyQualifiedTestName}");
  }

  public void ReportEndOfSuite(string suiteName)
  {
    _entries.Add("Ending suite " + suiteName);
  }

  public void Failed(FullyQualifiedTestName fullyQualifiedTestName, Exception failureRootCause)
  {
    _entries.Add($"Failed: {fullyQualifiedTestName}{Environment.NewLine}{failureRootCause}");
  }

  public void Starting(FullyQualifiedTestName fullyQualifiedTestName)
  {
    _entries.Add($"Starting: "+ $"{fullyQualifiedTestName}");
  }

  public void ReportStartOfSuite(string suiteName)
  {
    _entries.Add("Starting suite " + suiteName);
  }

  public void ReportToUser()
  {
    _destination.Send(_entries);
  }
}

//bug different languages
//bug different formats (e.g. JSON)