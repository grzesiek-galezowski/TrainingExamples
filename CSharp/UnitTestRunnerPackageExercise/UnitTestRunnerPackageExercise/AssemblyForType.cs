using System.Reflection;

namespace UnitTestRunnerPackageExercise;

public class AssemblyForType : ITestAssemblySource
{
  private readonly Type _type;

  public AssemblyForType(Type type)
  {
    _type = type;
  }

  public Assembly GetDll()
  {
    return _type.Assembly;
  }
}