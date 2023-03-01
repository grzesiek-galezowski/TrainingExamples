using System.Reflection;
using Core.NullableReferenceTypesExtensions;

namespace UnitTestRunnerPackageExercise;

internal static class TestSuiteFactory
{
  public static IEnumerable<TestSuite> CreateSuitesFrom(Assembly assembly)
  {
    return assembly.GetExportedTypes()
      .Where(t => !t.IsAbstract)
      .Select(t => new TestSuite(t.FullName.OrThrow(),
        TestCaseFactory.CreateTestCases(t)));
  }
}