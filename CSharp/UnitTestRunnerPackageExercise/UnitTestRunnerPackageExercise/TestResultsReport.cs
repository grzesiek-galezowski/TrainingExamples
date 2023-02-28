namespace UnitTestRunnerPackageExercise;

internal class TestResultsReport
{
  public void Passed(string @namespace, string suiteName, string testName)
  {
    Console.WriteLine($"Passed: {@namespace}.{suiteName}.{testName}");
  }

  public void Report()
  {
  }

  public void Failed(string @namespace, string suiteName, string testName, Exception failureRootCause)
  {
    Console.WriteLine($"Failed: {@namespace}.{suiteName}.{testName}{Environment.NewLine}{failureRootCause}");
  }

  public void Starting(string @namespace, string suiteName, string testName)
  {
    Console.WriteLine($"Starting: {@namespace}.{suiteName}.{testName}");
  }
}