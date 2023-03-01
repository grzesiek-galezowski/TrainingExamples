using System.Reflection;
using Core.NullableReferenceTypesExtensions;

namespace UnitTestRunnerPackageExercise;

public class TestCase
{
  private readonly MethodBase _methodInfo;
  private readonly FullyQualifiedTestName _fullyQualifiedTestName;

  public TestCase(MethodBase methodInfo)
  {
    _methodInfo = methodInfo;
    _fullyQualifiedTestName = new FullyQualifiedTestName(
      methodInfo.DeclaringType.OrThrow().Namespace ?? "global", 
      methodInfo.DeclaringType.OrThrow().Name, 
      methodInfo.Name);
  }

  public void Run(TestResultsReport results)
  {
    try
    {
      results.Starting(_fullyQualifiedTestName);

      _methodInfo.Invoke(Activator.CreateInstance(_methodInfo.DeclaringType.OrThrow()), null);
      results.Passed(_fullyQualifiedTestName);
    }
    catch (TargetInvocationException e)
    {
      results.Failed(_fullyQualifiedTestName, e.InnerException ?? new Exception("Unknown exception"));
    }
    catch (Exception e)
    {
      results.Failed(_fullyQualifiedTestName, e);
    }
  }
}