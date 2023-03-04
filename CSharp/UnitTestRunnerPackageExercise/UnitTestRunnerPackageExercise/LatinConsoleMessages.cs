namespace UnitTestRunnerPackageExercise;

public class LatinConsoleMessages : IConsoleReportMessages
{
  public string TestPassed(FullyQualifiedTestName fullyQualifiedTestName)
  {
    return $"Transivit: {fullyQualifiedTestName}";
  }

  public string EndOfSuite(string suiteName)
  {
    return "Finis examinum " + suiteName;
  }

  public string TestFailed(FullyQualifiedTestName fullyQualifiedTestName,
    Exception failureRootCause)
  {
    return $"Defecit: {fullyQualifiedTestName}{Environment.NewLine}{failureRootCause}";
  }

  public string StartingTest(FullyQualifiedTestName fullyQualifiedTestName)
  {
    return $"Incipit examen: " + $"{fullyQualifiedTestName}";
  }

  public string StartingSuite(string suiteName)
  {
    return "Incipit examinum " + suiteName;
  }
}