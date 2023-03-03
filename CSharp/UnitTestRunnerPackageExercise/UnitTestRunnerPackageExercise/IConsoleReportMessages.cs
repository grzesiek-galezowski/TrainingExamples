namespace UnitTestRunnerPackageExercise;

public interface IConsoleReportMessages
{
  string TestPassed(FullyQualifiedTestName fullyQualifiedTestName);
  string EndOfSuite(string suiteName);
  string TestFailed(FullyQualifiedTestName fullyQualifiedTestName, Exception failureRootCause);
  string StartingTest(FullyQualifiedTestName fullyQualifiedTestName);
  string StartingSuite(string suiteName);
}