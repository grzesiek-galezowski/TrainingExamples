using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace CourtesyImplementation1
{
  public interface Node
  {
  }

  public class Elephant : Node
  {
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
      int count = 0;
      foreach (var node in _content)
      {
        if (node is Box)
        {
          count += ((Box) node).GetElephantsCountInside();
        }
        else if(node is Elephant)
        {
          count += 1;
        }
      }
      return count;
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
