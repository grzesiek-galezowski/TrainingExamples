namespace CourtesyImplementation._3_Visitor
{
  public interface Node
  {
    void Accept(NodeVisitor visitor);
  }
}