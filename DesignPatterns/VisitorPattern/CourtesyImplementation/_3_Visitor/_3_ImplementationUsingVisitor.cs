using FluentAssertions;
using Xunit;

namespace CourtesyImplementation3
{
  public interface Node
  {
    void Accept(NodeVisitor visitor);
  }

  public interface NodeVisitor
  {
    void VisitElephant(Elephant elephant);
    void VisitBox(Box elephant);
  }

  public class Elephant : Node
  {
    public void Accept(NodeVisitor visitor)
    {
      visitor.VisitElephant(this);
    }
  }

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


  public class Lol
  {
    [Fact]
    public void Whatever_Anything()
    {
      //GIVEN
      var box = new Box(
        new Box(
          new Elephant()
        ), 
        new Box(
          new Box(
            new Elephant(),
            new Elephant(),
            new Elephant()
          ), 
          new Elephant()
        )
      );
      //WHEN
      var countingVisitor = new CountingVisitor();
      box.Accept(countingVisitor);

      //THEN
      countingVisitor.GetCount().Should().Be(5);
    }
  }

}
