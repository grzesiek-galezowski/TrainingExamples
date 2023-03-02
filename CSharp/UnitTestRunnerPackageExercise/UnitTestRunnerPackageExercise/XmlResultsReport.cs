namespace UnitTestRunnerPackageExercise;

//bug use it somewhere
public class XmlResultsReport : ITestResultsReport
{
  public void Passed(FullyQualifiedTestName fullyQualifiedTestName)
  {
    throw new NotImplementedException();
  }

  public void ReportEndOfSuite(string suiteName)
  {
    throw new NotImplementedException();
  }

  public void Failed(FullyQualifiedTestName fullyQualifiedTestName, Exception failureRootCause)
  {
    throw new NotImplementedException();
  }

  public void Starting(FullyQualifiedTestName fullyQualifiedTestName)
  {
    throw new NotImplementedException();
  }

  public void ReportStartOfSuite(string suiteName)
  {
    throw new NotImplementedException();
  }

  public void ReportToUser()
  {
    throw new NotImplementedException();
  }
}