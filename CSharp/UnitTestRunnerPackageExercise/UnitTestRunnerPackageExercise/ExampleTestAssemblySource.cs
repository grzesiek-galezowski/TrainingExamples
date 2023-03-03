using System.Reflection;

namespace UnitTestRunnerPackageExercise;

public class ExampleTestAssemblySource : ITestAssemblySource
{
  public Assembly GetDll()
  {
    return Assembly.LoadFile(
      "C:\\Users\\HYPERBOOK\\Documents\\GitHub\\TrainingExamples\\CSharp\\UnitTestRunnerPackageExercise\\ExampleTests\\bin\\Debug\\net7.0\\ExampleTests.dll");
  }
}