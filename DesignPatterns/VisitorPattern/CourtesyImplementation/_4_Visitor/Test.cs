using FluentAssertions;
using Xunit;

namespace CourtesyImplementation._4_Visitor
{
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