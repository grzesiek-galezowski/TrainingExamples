namespace UnitTestRunnerPackageExercise;

public class EnglishConsoleMessages : IConsoleReportMessages
{
  public string TestPassed(FullyQualifiedTestName fullyQualifiedTestName)
  {
    return $"Passed: {fullyQualifiedTestName}";
  }

  public string EndOfSuite(string suiteName)
  {
    return "Ending suite " + suiteName;
  }

  public string TestFailed(FullyQualifiedTestName fullyQualifiedTestName, Exception failureRootCause)
  {
    return $"Failed: {fullyQualifiedTestName}{Environment.NewLine}{failureRootCause}";
  }

  public string StartingTest(FullyQualifiedTestName fullyQualifiedTestName)
  {
    return $"Starting: "+ $"{fullyQualifiedTestName}";
  }

  public string StartingSuite(string suiteName)
  {
    return "Starting suite " + suiteName;
  }
}