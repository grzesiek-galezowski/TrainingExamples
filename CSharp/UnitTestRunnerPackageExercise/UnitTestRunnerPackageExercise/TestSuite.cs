namespace UnitTestRunnerPackageExercise;

public class TestSuite
{
  private readonly string _suiteName;
  private readonly IEnumerable<TestCase> _testCases;

  public TestSuite(string suiteName, IEnumerable<TestCase> testCases)
  {
    _suiteName = suiteName;
    _testCases = testCases;
  }

  public void Run(ITestResultsReport results)
  {
    results.StartSuite(_suiteName);
    foreach (var testCase in _testCases)
    {
      testCase.Run(results);
    }

    results.EndOfSuite(_suiteName);
  }
}