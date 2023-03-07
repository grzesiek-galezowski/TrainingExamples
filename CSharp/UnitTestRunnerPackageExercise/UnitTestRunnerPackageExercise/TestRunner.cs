namespace UnitTestRunnerPackageExercise;

public class TestRunner
{
  public ITestResults Results { get; init; } =
    new TextBasedResultsReport(new EnglishConsoleMessages(), new ConsoleDestination());
  public ITestAssemblySource TestAssemblySource { get; init; } = new ExampleTestAssemblySource();

  public void Run()
  {
    var assembly = TestAssemblySource.GetDll();
    var testSet = TestSetFactory.CreateTestSet(assembly);
    testSet.Run(Results);
    Results.ReportToUser();
  }

}