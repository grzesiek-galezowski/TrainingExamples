namespace CourtesyImplementation._4_Visitor
{
  public class Elephant : Node
  {
    public void Accept(NodeVisitor visitor)
    {
      visitor.VisitElephant(this);
    }
  }
}
