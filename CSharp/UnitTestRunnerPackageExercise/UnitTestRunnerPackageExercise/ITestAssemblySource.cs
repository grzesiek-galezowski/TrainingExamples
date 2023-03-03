using System.Reflection;

namespace UnitTestRunnerPackageExercise;

public interface ITestAssemblySource
{
  Assembly GetDll();
}