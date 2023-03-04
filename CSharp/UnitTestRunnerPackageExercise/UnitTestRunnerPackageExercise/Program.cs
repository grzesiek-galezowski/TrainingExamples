namespace UnitTestRunnerPackageExercise;

public class Program
{
  public static void Main(string[] args)
  {
    new TestRunner().Run();
  }

  public static void OtherExamples(string[] args)
  {
    new TestRunner
    {
      Results = new TextBasedResultsReport(
        new EnglishConsoleMessages(),
        new FlatFileDestination()),
      TestAssemblySource = new TestAssemblyPath("C:\\Users\\HYPERBOOK\\Documents\\GitHub\\TrainingExamples\\CSharp\\UnitTestRunnerPackageExercise\\ExampleTests\\bin\\Debug\\net7.0\\ExampleTests.dll")
    }.Run();

    new TestRunner
    {
      Results = new TextBasedResultsReport(
        new PolishConsoleMessages(),
        new FlatFileDestination()),
      TestAssemblySource = new ThisAssemblySource()
    }.Run();
  
    new TestRunner
    {
      Results = new TextBasedResultsReport(
        new PolishConsoleMessages(),
        new ConsoleDestination())
    }.Run();

    new TestRunner
    {
      Results = new TextBasedResultsReport(
        new KoreanConsoleMessages(),
        new ConsoleDestination())
    }.Run();

    new TestRunner
    {
      Results = new TextBasedResultsReport(
        new LatinConsoleMessages(),
        new ConsoleDestination())
    }.Run();

    new TestRunner
    {
      Results = new TextBasedResultsReport(
        new FrenchConsoleMessages(),
        new ConsoleDestination())
    }.Run();

    new TestRunner
    {
      Results = new TextBasedResultsReport(
        new ItalianConsoleMessages(),
        new ConsoleDestination())
    }.Run();


    new TestRunner
    {
      Results = new StructuredReport(
        new NewtonsoftJsonResultsTextFormat(),
        new SqLiteResultsDestination("Data Source=.\\Results.db"))
    }.Run();
  
    new TestRunner
    {
      Results = new StructuredReport(
        new SystemTextJsonResultsTextFormat(), 
        new LiteDbDestination(Path.GetTempFileName()))
    }.Run();
  
    new TestRunner
    {
      Results = new TextBasedResultsReport(
        new PolishConsoleMessages(),
        new ConsoleDestination())
    }.Run();
  }

}

//TODO assertions library + some with external dependencies (e.g. JSON assertions)
//TODO documentation generator - generate a document without