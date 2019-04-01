using FluentAssertions;
using Xunit;

namespace CourtesyImplementation._2_CourtesyImplementation
{
  public class Test
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