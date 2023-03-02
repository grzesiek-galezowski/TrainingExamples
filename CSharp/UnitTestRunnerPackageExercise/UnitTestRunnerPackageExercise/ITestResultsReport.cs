namespace UnitTestRunnerPackageExercise;

public interface ITestResultsReport
{
  void Passed(FullyQualifiedTestName fullyQualifiedTestName);
  void ReportEndOfSuite(string suiteName);
  void Failed(FullyQualifiedTestName fullyQualifiedTestName, Exception failureRootCause);
  void Starting(FullyQualifiedTestName fullyQualifiedTestName);
  void ReportStartOfSuite(string suiteName);
  void ReportToUser();
}