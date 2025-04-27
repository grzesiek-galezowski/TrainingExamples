using NSubstitute;
using NUnit.Framework;
using TddEbook.TddToolkit;

namespace LED1
{
  class SwitchableSegmentSpecification
  {
    [Test]
    public void ShouldOutputBlankCharWhenEvaluatedForArrayThatDoesNotContainTrigger()
    {
      //GIVEN
      var onTrigger = Any.Char();
      var fallbackValue = Any.String();
      var fallback = Substitute.For<Displayable>();
      var inputTriggers = Any.ArrayWithout(onTrigger);
      var segment = new SwitchableSegment(onTrigger, Any.String(), fallback);

      fallback.Evaluate(inputTriggers).Returns(fallbackValue);

      //WHEN
      var result = segment.Evaluate(inputTriggers);

      //THEN
      XAssert.Equal(fallbackValue, result);
    }

    [Test]
    public void ShouldOutputOnValueWhenEvaluatedForArrayThatContainsTrigger()
    {
      //GIVEN
      var onTrigger = Any.Char();
      var onValue = Any.String();
      var inputTriggers = Any.ArrayWith(onTrigger);
      var fallback = Substitute.For<Displayable>();
      var segment = new SwitchableSegment(onTrigger, onValue, new BlankSpace());

      fallback.Evaluate(inputTriggers).Returns(Any.StringOtherThan(onValue));

      //WHEN
      var result = segment.Evaluate(inputTriggers);

      //THEN
      XAssert.Equal(onValue, result);
    }

    //TODO deal with "."

  }
}
