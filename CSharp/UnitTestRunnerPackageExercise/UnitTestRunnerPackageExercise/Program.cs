using System.Reflection;
using Core.NullableReferenceTypesExtensions;

namespace UnitTestRunnerPackageExercise;

internal class Program
{
  public static void Main(string[] args)
  {
    var results = new TestResultsReport();
    var assembly = Assembly.LoadFile("C:\\Users\\HYPERBOOK\\Documents\\GitHub\\TrainingExamples\\CSharp\\UnitTestRunnerPackageExercise\\ExampleTests\\bin\\Debug\\net7.0\\ExampleTests.dll");
    var exportedTypes = assembly.GetExportedTypes().Where(t => !t.IsAbstract);
    foreach (var exportedType in exportedTypes)
    {
      var methodInfos = exportedType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly).OrThrow();
      foreach (var methodInfo in methodInfos)
      {
        try
        {
          results.Starting(
            methodInfo.DeclaringType.OrThrow().Namespace ?? "global",
            methodInfo.DeclaringType.OrThrow().Name,
            methodInfo.Name);

          methodInfo.Invoke(Activator.CreateInstance(methodInfo.DeclaringType.OrThrow()), null);
          results.Passed(
            methodInfo.DeclaringType.OrThrow().Namespace ?? "global",
            methodInfo.DeclaringType.OrThrow().Name,
            methodInfo.Name);
        }
        catch (TargetInvocationException e)
        {
          results.Failed(
            methodInfo.DeclaringType.OrThrow().Namespace ?? "global",
            methodInfo.DeclaringType.OrThrow().Name,
            methodInfo.Name,
            e.InnerException ?? new Exception("Unknown exception"));
        }
        catch (Exception e)
        {
          results.Failed(
            methodInfo.DeclaringType.OrThrow().Namespace ?? "global",
            methodInfo.DeclaringType.OrThrow().Name,
            methodInfo.Name,
            e);
        }
      }

      results.Report();
    }
  }
}

//TODO assertions library + some with external dependencies
//TODO different report destinations (file vs console)
//TODO different runners (GUI vs Console)
//TODO different report formats (text vs JSON vs XML?)