namespace CourtesyImplementation._4_Visitor
{
  public class Box : Node
  {
    private readonly Node[] _content;

    public Box(params Node[] content)
    {
      _content = content;
    }

    public void Accept(NodeVisitor visitor)
    {
      visitor.VisitBox(this);
      foreach (var node in _content)
      {
        node.Accept(visitor);
      }
    }
  }
}