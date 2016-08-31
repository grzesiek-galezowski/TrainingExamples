namespace CourtesyImplementation._4_Visitor
{
  public interface Node
  {
    void Accept(NodeVisitor visitor);
  }
}