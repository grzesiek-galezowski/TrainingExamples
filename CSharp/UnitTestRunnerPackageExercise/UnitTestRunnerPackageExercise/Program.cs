using System.Reflection;

namespace UnitTestRunnerPackageExercise;

internal class Program
{
  public static void Main(string[] args)
  {
    var results = new TestResultsReport();
    var assembly = Assembly.LoadFile("C:\\Users\\HYPERBOOK\\Documents\\GitHub\\TrainingExamples\\CSharp\\UnitTestRunnerPackageExercise\\ExampleTests\\bin\\Debug\\net7.0\\ExampleTests.dll");
    TestSetFactory.CreateTestSet(assembly).Run(results);
  }
}

//TODO assertions library + some with external dependencies
//TODO different report destinations (file vs console)
//TODO different runners (GUI vs Console)
//TODO different report formats (text vs JSON vs XML?)