namespace CourtesyImplementation._3_Visitor
{
  public class Elephant : Node
  {
    public void Accept(NodeVisitor visitor)
    {
      visitor.VisitElephant(this);
    }
  }
}
