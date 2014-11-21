namespace EventAnomalies.Example2
{
  public class ExampleUsage
  {
    public void Main()
    {
      var nonsense = new Nonsense2(new ConsoleErrorService());
      nonsense.Process(123);
      nonsense.Dispose();
    }
  }
}