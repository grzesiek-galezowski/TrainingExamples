namespace CourtesyImplementation._3_Visitor
{
  public class CountingVisitor : NodeVisitor
  {
    private int _count = 0;

    public void VisitElephant(Elephant elephant)
    {
      _count++;
    }

    public void VisitBox(Box elephant)
    {

    }

    public int GetCount()
    {
      return _count;
    }
  }
}