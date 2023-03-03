using System.Collections.Immutable;

namespace UnitTestRunnerPackageExercise;

public class TestSetDtoBuilder
{
  private readonly List<TestSuiteDtoBuilder> _testSuiteDtoBuilders = new();

  public void CurrentTestPassed()
  {
    _testSuiteDtoBuilders.Last().CurrentTestPassed();
  }

  public void TestFailed(Exception failureRootCause)
  {
    _testSuiteDtoBuilders.Last().CurrentTestFailed(failureRootCause);
  }

  public void StartingTest(FullyQualifiedTestName fullyQualifiedTestName)
  {
    _testSuiteDtoBuilders.Last().TestIsStarting(fullyQualifiedTestName);
  }

  public void StartSuite(string suiteName)
  {
    _testSuiteDtoBuilders.Add(new TestSuiteDtoBuilder(suiteName));
  }

  public TestSetDto Build()
  {
    return new TestSetDto(_testSuiteDtoBuilders.Select(b => b.Build()).ToImmutableArray());
  }
}