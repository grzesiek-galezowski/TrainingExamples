using NSubstitute;
using NUnit.Framework;
using TddEbook.TddToolkit;
using TddEbook.TddToolkit.NSubstitute;

namespace LED1
{
  public class DriverSpecification
  {
    [Test]
    public void ShouldPutTheResultOfEvaluatingDisplayablesOnDisplay()
    {
      //GIVEN
      var display = Substitute.For<Display>();
      var inputTriggers = Any.Array<char>();
      var result1 = Any.String();
      var result2 = Any.String();
      var result3 = Any.String();

      var rows = DisplayablesThatReturnFor(inputTriggers, result1, result2, result3);
      var driver = new Driver(display, rows);
      
      //WHEN
      driver.Display(inputTriggers);

      //THEN
      display.Received(1).Put(result1, result2, result3);
      XReceived.Only(() => display.Put(result1, result2, result3));
    }

    private static Displayable[] DisplayablesThatReturnFor(char[] inputTriggers, string result1, string result2, string result3)
    {
      var displayable1 = Substitute.For<Displayable>();
      var displayable2 = Substitute.For<Displayable>();
      var displayable3 = Substitute.For<Displayable>();
      displayable1.Evaluate(inputTriggers).Returns(result1);
      displayable2.Evaluate(inputTriggers).Returns(result2);
      displayable3.Evaluate(inputTriggers).Returns(result3);

      var rows = new[] {displayable1, displayable2, displayable3};
      return rows;
    }
  }
}
