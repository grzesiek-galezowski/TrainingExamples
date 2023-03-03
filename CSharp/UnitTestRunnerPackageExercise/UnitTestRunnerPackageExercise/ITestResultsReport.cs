namespace UnitTestRunnerPackageExercise;

public interface ITestResultsReport
{
  void TestPassed(FullyQualifiedTestName fullyQualifiedTestName);
  void EndOfSuite(string suiteName);
  void TestFailed(FullyQualifiedTestName fullyQualifiedTestName, Exception failureRootCause);
  void StartingTest(FullyQualifiedTestName fullyQualifiedTestName);
  void StartSuite(string suiteName);
  void ReportToUser();
}