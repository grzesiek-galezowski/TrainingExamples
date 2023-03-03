namespace UnitTestRunnerPackageExercise;

public class TextBasedResultsReport : ITestResults
{
  private readonly List<string> _entries = new();
  private readonly ITestResultsDestination _destination;
  private readonly IConsoleReportMessages _messages;

  public TextBasedResultsReport(
    IConsoleReportMessages englishConsoleMessages, 
    ITestResultsDestination flatFileDestination)
  {
    _destination = flatFileDestination;
    _messages = englishConsoleMessages;
  }

  public void TestPassed(FullyQualifiedTestName fullyQualifiedTestName)
  {
    _entries.Add(_messages.TestPassed(fullyQualifiedTestName));
  }

  public void EndOfSuite(string suiteName)
  {
    _entries.Add(_messages.EndOfSuite(suiteName));
  }

  public void TestFailed(FullyQualifiedTestName fullyQualifiedTestName, Exception failureRootCause)
  {
    _entries.Add(_messages.TestFailed(fullyQualifiedTestName, failureRootCause));
  }

  public void StartingTest(FullyQualifiedTestName fullyQualifiedTestName)
  {
    _entries.Add(_messages.StartingTest(fullyQualifiedTestName));
  }

  public void StartSuite(string suiteName)
  {
    _entries.Add(_messages.StartingSuite(suiteName));
  }

  public void ReportToUser()
  {
    _destination.Send(string.Join(Environment.NewLine, _entries));
  }
}

//bug different languages (e.g. Polish, English)
//bug different formats (e.g. JSON)