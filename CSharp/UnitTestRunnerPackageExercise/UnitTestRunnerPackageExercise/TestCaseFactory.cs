using System.Reflection;
using Core.NullableReferenceTypesExtensions;

namespace UnitTestRunnerPackageExercise;

public static class TestCaseFactory
{
  public static IEnumerable<TestCase> CreateTestCases(Type exportedType)
  {
    return exportedType.GetMethods(
        BindingFlags.Instance | 
        BindingFlags.Public | 
        BindingFlags.DeclaredOnly).OrThrow()
      .Select(m => new TestCase(m));
  }
}