using System.Collections.Immutable;
using Core.Maybe;

namespace UnitTestRunnerPackageExercise;

public class TestSuiteDtoBuilder //bug a Build method
{
  private readonly string _suiteName;
  private readonly List<TestReportDto> _tests = new();

  public TestSuiteDtoBuilder(string suiteName)
  {
    _suiteName = suiteName;
  }

  public void CurrentTestPassed()
  {
    _tests[^1] = _tests[^1] with
    {
      Status = TestStatus.Passed
    };
  }

  public void CurrentTestFailed(Exception failureRootCause)
  {
    _tests[^1] = _tests[^1] with
    {
      Status = TestStatus.Failed,
      Exception = failureRootCause.Just()
    };
  }

  public void TestIsStarting(FullyQualifiedTestName fullyQualifiedTestName)
  {
    _tests.Add(new TestReportDto(fullyQualifiedTestName));
  }

  public TestSuiteDto Build()
  {
    return new TestSuiteDto(_suiteName, _tests.ToImmutableArray());
  }
}