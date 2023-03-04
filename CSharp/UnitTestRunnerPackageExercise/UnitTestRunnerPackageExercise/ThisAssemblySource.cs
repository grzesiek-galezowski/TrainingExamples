using System.Reflection;

namespace UnitTestRunnerPackageExercise;

public class ThisAssemblySource : ITestAssemblySource
{
  public Assembly GetDll()
  {
    return Assembly.GetExecutingAssembly();
  }
}