namespace EventAnomalies.Example3
{
  public class ExampleUsage
  {
    public void Main()
    {
      var nonsense = new Nonsense3();
      var errorService = new ConsoleErrorService(nonsense);
      nonsense.Process(123);
      errorService.Dispose();
    }
  }
}
