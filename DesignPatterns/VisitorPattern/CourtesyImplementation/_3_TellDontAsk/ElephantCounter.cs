namespace CourtesyImplementation._3_TellDontAsk
{
  public class ElephantCounter
  {
    private int _count = 0;

    public void Add()
    {
      _count++;
    }

    public int Value()
    {
      return _count;
    }
  }
}