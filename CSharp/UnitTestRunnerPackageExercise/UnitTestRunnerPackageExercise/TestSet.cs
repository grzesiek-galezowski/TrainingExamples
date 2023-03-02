namespace UnitTestRunnerPackageExercise;

public class TestSet
{
  private readonly IEnumerable<TestSuite> _suites;

  public TestSet(IEnumerable<TestSuite> suites)
  {
    _suites = suites;
  }

  public void Run(ITestResultsReport results)
  {
    foreach (var suite in _suites)
    {
      suite.Run(results);
    }
  }
}