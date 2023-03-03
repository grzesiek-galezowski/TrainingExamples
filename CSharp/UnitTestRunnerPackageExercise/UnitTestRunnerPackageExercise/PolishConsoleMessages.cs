namespace UnitTestRunnerPackageExercise;

public class PolishConsoleMessages : IConsoleReportMessages
{
  public string TestPassed(FullyQualifiedTestName fullyQualifiedTestName)
  {
    return $"Przeszedł: {fullyQualifiedTestName}";
  }

  public string EndOfSuite(string suiteName)
  {
    return "Koniec zestawu " + suiteName;
  }

  public string TestFailed(FullyQualifiedTestName fullyQualifiedTestName, Exception failureRootCause)
  {
    return $"Padł: {fullyQualifiedTestName}{Environment.NewLine}{failureRootCause}";
  }

  public string StartingTest(FullyQualifiedTestName fullyQualifiedTestName)
  {
    return $"Zaczynamy test: "+ $"{fullyQualifiedTestName}";
  }

  public string StartingSuite(string suiteName)
  {
    return "Zaczynamy zestaw " + suiteName;
  }

}