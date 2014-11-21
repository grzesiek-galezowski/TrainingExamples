namespace EventAnomalies.Example4
{
  public class ExampleUsage
  {
    public void Main()
    {
      var makesSense = new MakesSense();
      var errorService = new ConsoleErrorService();

      makesSense.SomethingHappened += errorService.LogError;
     
      makesSense.Process(123);

      makesSense.SomethingHappened -= errorService.LogError;
    }
  }
}