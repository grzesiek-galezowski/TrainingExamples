using System.Reflection;

namespace UnitTestRunnerPackageExercise;

internal class Program
{
  public static void Main(string[] args)
  {
    var results = new ConsoleResultsReport();
    var assembly = Assembly.LoadFile("C:\\Users\\HYPERBOOK\\Documents\\GitHub\\TrainingExamples\\CSharp\\UnitTestRunnerPackageExercise\\ExampleTests\\bin\\Debug\\net7.0\\ExampleTests.dll");
    var testSet = TestSetFactory.CreateTestSet(assembly);
    testSet.Run(results);
    results.ReportToUser();
  }

  public static void Main2(string[] args)
  {
    var results = new XmlResultsReport(); //bug or structured and then have a formatted and destination
    var assembly = Assembly.LoadFile("C:\\Users\\HYPERBOOK\\Documents\\GitHub\\TrainingExamples\\CSharp\\UnitTestRunnerPackageExercise\\ExampleTests\\bin\\Debug\\net7.0\\ExampleTests.dll");
    var testSet = TestSetFactory.CreateTestSet(assembly);
    testSet.Run(results);
    results.ReportToUser();
  }
}

//TODO assertions library + some with external dependencies (e.g. JSON assertions)
//TODO different report destinations (file vs console)
//TODO different runners (GUI vs Console)
//TODO different report formats (text vs JSON vs XML?)