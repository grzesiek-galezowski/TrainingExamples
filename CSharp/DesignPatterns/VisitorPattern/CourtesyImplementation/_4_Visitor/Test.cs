using FluentAssertions;
using Xunit;

namespace CourtesyImplementation._4_Visitor
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
          new Box( //what if we add melons?
            new Elephant(),
            new Elephant(),
            new Elephant()
          ), 
          new Elephant()
        )
      );
      //WHEN
      var countingVisitor = new ElephantCountingVisitor();
      box.Accept(countingVisitor);

      //THEN
      countingVisitor.GetCount().Should().Be(5);
    }
  }
}