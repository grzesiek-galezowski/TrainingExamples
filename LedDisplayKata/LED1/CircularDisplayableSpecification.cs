using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using TddEbook.TddToolkit;

namespace LED1
{
  class CircularDisplayableSpecification
  {
    [Test]
    public void ShouldYieldEvaluationResultsInCircle()
    {
      //GIVEN
      Displayable d1 = Substitute.For<Displayable>();
      Displayable d2 = Substitute.For<Displayable>();
      Displayable d3 = Substitute.For<Displayable>();
      var displayable = new CircularDisplayable(d1, d2, d3);
      var input = Any.Array<char>();

      var d1Result = Any.String();
      var d2Result = Any.String();
      var d3Result = Any.String();

      d1.Evaluate(input).Returns(d1Result);
      d2.Evaluate(input).Returns(d2Result);
      d3.Evaluate(input).Returns(d3Result);

      //WHEN
      var result1 = displayable.Evaluate(input);
      var result2 = displayable.Evaluate(input);
      var result3 = displayable.Evaluate(input);
      var result4 = displayable.Evaluate(input);

      //THEN
      XAssert.Equal(d1Result, result1);
      XAssert.Equal(d2Result, result2);
      XAssert.Equal(d3Result, result3);
      XAssert.Equal(d1Result, result4);
    }
  }
}
