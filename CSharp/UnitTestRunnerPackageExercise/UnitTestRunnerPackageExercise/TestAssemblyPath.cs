using System.Reflection;

namespace UnitTestRunnerPackageExercise;

public class TestAssemblyPath : ITestAssemblySource
{
  private readonly string _path;

  public TestAssemblyPath(string path)
  {
    _path = path;
  }

  public Assembly GetDll()
  {
    return Assembly.LoadFile(
      _path);
  }
}