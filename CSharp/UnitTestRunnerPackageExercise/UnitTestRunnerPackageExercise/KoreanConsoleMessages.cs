namespace UnitTestRunnerPackageExercise;

public class KoreanConsoleMessages : IConsoleReportMessages
{
  public string TestPassed(FullyQualifiedTestName fullyQualifiedTestName)
  {
    return $"통과 : {fullyQualifiedTestName}";
  }

  public string EndOfSuite(string suiteName)
  {
    return suiteName + "테스트 그룹 종료";
  }

  public string TestFailed(FullyQualifiedTestName fullyQualifiedTestName,
    Exception failureRootCause)
  {
    return $"실패 : {fullyQualifiedTestName}{Environment.NewLine}{failureRootCause}";
  }

  public string StartingTest(FullyQualifiedTestName fullyQualifiedTestName)
  {
    return $"시작하는 테스트: " + $"{fullyQualifiedTestName}";
  }

  public string StartingSuite(string suiteName)
  {
    return suiteName + "테스트 그룹 시작";
  }
}