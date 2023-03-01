using System.Reflection;

namespace UnitTestRunnerPackageExercise;

static internal class TestSetFactory
{
  public static TestSet CreateTestSet(Assembly assembly)
  {
    return new TestSet(TestSuiteFactory.CreateSuitesFrom(assembly));
  }
}