namespace UnitTestRunnerPackageExercise;

public class Program
{
  public static void Main(string[] args)
  {
    new TestRunner().Run();
  }

  public static void Main_1(string[] args)
  {
    new TestRunner
    {
      Results = new TextBasedResultsReport(
        new EnglishConsoleMessages(),
        new FlatFileDestination()),
      TestAssemblySource = new TestAssemblyPath("C:\\Users\\HYPERBOOK\\Documents\\GitHub\\TrainingExamples\\CSharp\\UnitTestRunnerPackageExercise\\ExampleTests\\bin\\Debug\\net7.0\\ExampleTests.dll")
    }.Run();
  }

  public static void Main_2(string[] args)
  {
    new TestRunner
    {
      Results = new TextBasedResultsReport(
        new PolishConsoleMessages(),
        new FlatFileDestination())
    }.Run();
  }

  public static void Main_3(string[] args)
  {
    new TestRunner
    {
      Results = new TextBasedResultsReport(
        new PolishConsoleMessages(),
        new ConsoleDestination())
    }.Run();
  }

  public static void Main_4(string[] args)
  {
    new TestRunner
    {
      Results = new StructuredReport(
        new NewtonsoftJsonResultsTextFormat(),
        new SqLiteResultsDestination("Data Source=.\\Results.db"))
    }.Run();
  }

  public static void Main_5(string[] args)
  {
    new TestRunner
    {
      Results = new StructuredReport(
        new SystemTextJsonResultsTextFormat(), 
        new LiteDbDestination(Path.GetTempFileName()))
    }.Run();
  }
}

//TODO assertions library + some with external dependencies (e.g. JSON assertions)
//TODO different runners (GUI vs Console)
//TODO documentation generator - generate a document without