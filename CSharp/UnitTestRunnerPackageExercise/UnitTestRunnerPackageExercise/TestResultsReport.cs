namespace UnitTestRunnerPackageExercise;

public class TestResultsReport
{
  public void Passed(FullyQualifiedTestName fullyQualifiedTestName)
  {
    Console.WriteLine($"Passed: {fullyQualifiedTestName}");
  }

  public void ReportEndOfSuite(string suiteName)
  {
    Console.WriteLine("Ending suite " + suiteName);
  }

  public void Failed(FullyQualifiedTestName fullyQualifiedTestName, Exception failureRootCause)
  {
    Console.WriteLine($"Failed: {fullyQualifiedTestName}{Environment.NewLine}{failureRootCause}");
  }

  public void Starting(FullyQualifiedTestName fullyQualifiedTestName)
  {
    Console.WriteLine($"Starting: "+ $"{fullyQualifiedTestName}");
  }

  public void ReportStartOfSuite(string suiteName)
  {
    Console.WriteLine("Starting suite " + suiteName);
  }
}