using System.Linq;
using FluentAssertions;
using Xunit;

namespace CourtesyImplementation2
{
  public interface Node
  {
    int GetElephantsCountInside();
  }

  public class Elephant : Node
  {
    public int GetElephantsCountInside()
    {
      return 1;
    }
  }

  public class Box : Node
  {
    private readonly Node[] _content;

    public Box(params Node[] content)
    {
      _content = content;
    }

    public int GetElephantsCountInside()
    {
      return _content.Sum(b => b.GetElephantsCountInside());
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
      var count = box.GetElephantsCountInside();

      //THEN
      count.Should().Be(5);
    }
  }

}
