namespace UnitTestRunnerPackageExercise;

public class ItalianConsoleMessages : IConsoleReportMessages
{
  public string TestPassed(FullyQualifiedTestName fullyQualifiedTestName)
  {
    return $"Superato: {fullyQualifiedTestName}";
  }

  public string EndOfSuite(string suiteName)
  {
    return "Fine del gruppo di test " + suiteName;
  }

  public string TestFailed(FullyQualifiedTestName fullyQualifiedTestName,
    Exception failureRootCause)
  {
    return $"Fallito: {fullyQualifiedTestName}{Environment.NewLine}{failureRootCause}";
  }

  public string StartingTest(FullyQualifiedTestName fullyQualifiedTestName)
  {
    return $"Inizio del test: " + $"{fullyQualifiedTestName}";
  }

  public string StartingSuite(string suiteName)
  {
    return "Inizio del gruppo di test " + suiteName;
  }
}