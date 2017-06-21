namespace EventAnomalies.Example3
{
  public class Nonsense3
  {
    public event ErrorHandling SomethingHappened;

    public void Process(int argument)
    {
      if (argument != 1)
      {
        this.SomethingHappened("someone is trying to cheat on me!");
      }
    }
  }
}
