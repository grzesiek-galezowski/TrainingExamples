using FluentAssertions;
using Xunit;

namespace CourtesyImplementation._3_TellDontAsk
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
            new Melon(),
            new Elephant()
          ), 
          new Elephant()
        )
      );
      var elephantCounter = new ElephantCounter();

      //WHEN
      box.AddTo(elephantCounter);
      var count = elephantCounter.Value();

      //THEN
      count.Should().Be(5);
    }
  }
}