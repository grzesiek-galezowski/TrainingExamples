using System.Reflection;

namespace UnitTestRunnerPackageExercise;

public static class TestSetFactory
{
  public static TestSet CreateTestSet(Assembly assembly)
  {
    return new TestSet(TestSuiteFactory.CreateSuitesFrom(assembly));
  }
}