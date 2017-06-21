namespace AutomatedRefactorings._3_LessUsed
{
  public class MyMessage
  {
    private readonly int _i;
    private readonly int _i1;

    public MyMessage(int i, int i1)
    {
      _i = i;
      _i1 = i1;
    }

    public int Prop1
    {
      get { return _i; }
    }

    public int Prop2
    {
      get { return _i1; }
    }

    public void Send()
    {
      throw new System.NotImplementedException();
    }
  }
}