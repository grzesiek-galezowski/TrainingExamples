namespace UnitTestRunnerPackageExercise;

public class StructuredReport : ITestResults
{
  private readonly TestSetDtoBuilder _testSetDtoBuilder = new();
  private readonly IResultsTextFormat _textFormat;
  private readonly ITestResultsDestination _destination;

  public StructuredReport(IResultsTextFormat textFormat, ITestResultsDestination destination)
  {
    _textFormat = textFormat;
    _destination = destination;
  }

  public void TestPassed(FullyQualifiedTestName fullyQualifiedTestName)
  {
    _testSetDtoBuilder.CurrentTestPassed();
  }

  public void EndOfSuite(string suiteName)
  {
    
  }

  public void TestFailed(FullyQualifiedTestName fullyQualifiedTestName, Exception failureRootCause)
  {
    _testSetDtoBuilder.TestFailed(failureRootCause);
  }

  public void StartingTest(FullyQualifiedTestName fullyQualifiedTestName)
  {
    _testSetDtoBuilder.StartingTest(fullyQualifiedTestName);
  }

  public void StartSuite(string suiteName)
  {
    _testSetDtoBuilder.StartSuite(suiteName);
  }

  public void ReportToUser()
  {
    var dto = _testSetDtoBuilder.Build();
    var stringRepresentation = _textFormat.ApplyTo(dto);
    _destination.Send(stringRepresentation);
  }
}