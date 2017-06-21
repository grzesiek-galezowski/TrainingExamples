using NSubstitute;
using NUnit.Framework;
using TddEbook.TddToolkit.NSubstitute;

//bug . Acceptance Specification
//bug . blank display
//bug . all single segments
//bug . all segments at the same time
//bug . Move to separate class Segment
//bug . Move segment creation to factory method and replace constructor calls
//bug . Add unit tests for Segment
//bug . Extract interface Displayable
//bug . Introduce BlankSpace : Displayable
//bug . change addition of strings to array aggregation
//bug . introduce Row abstraction and move up to newly created factory method
//bug . convert rows to array, make it a parameter of driver and move creation of full app into composition root
//bug . deal with "." duplication
//bug . write unit test for Driver itself
//bug . new feature - second time display '*' and third time display the trigger of the segment


namespace LED1
{
  public class AcceptanceSpecification
  {
    [TestCase(new char[] {}, new[]
      {
        "...",
        "...",
        "...",
        "...",
        "..."
      })]
    [TestCase(new char[] {'A'}, new[]
      {
        ".-.",
        "...",
        "...",
        "...",
        "..."
      })]
    [TestCase(new char[] {'B'}, new[]
      {
        "...",
        "..|",
        "...",
        "...",
        "..."
      })]
    [TestCase(new char[] {'C'}, new[]
      {
        "...",
        "...",
        "...",
        "..|",
        "..."
      })]
    [TestCase(new char[] {'D'}, new[]
      {
        "...",
        "...",
        "...",
        "...",
        ".-."
      })]
    [TestCase(new char[] {'E'}, new[]
      {
        "...",
        "...",
        "...",
        "|..",
        "..."
      })]
    [TestCase(new char[] {'F'}, new[]
      {
        "...",
        "|..",
        "...",
        "...",
        "..."
      })]
    [TestCase(new char[] {'G'}, new[]
      {
        "...",
        "...",
        ".-.",
        "...",
        "..."
      })]
    [TestCase(new char[] {'A', 'B', 'C', 'D', 'E', 'F', 'G'}, new[]
      {
        ".-.",
        "|.|",
        ".-.",
        "|.|",
        ".-."
      })]
    public void ShouldPassLedDrawingToDisplayWithSegmentsCorrespondingToInputIndices(char[] input, string[] expected)
    {
      //GIVEN
      var display = Substitute.For<Display>();
      var driver = CompositionRoot.CreateDefaultDriver(display);

      //WHEN
      driver.Display(input);

      //THEN
      display.Received(1).Put(expected);
      display.Received(0).Put(XArg.IsNotLike(expected));
    }


    [Test]
    public void ShouldPassThreeTypesOfDrawingsToDisplayInCycles()
    {
      //GIVEN
      var display = Substitute.For<Display>();
      var driver = CompositionRoot.CreateDefaultDriver(display);

      var expected1 = new []
      {
        ".-.",
        "|.|",
        ".-.",
        "|.|",
        ".-."
      };
      var expected2 = new []
      {
        ".*.",
        "*.*",
        ".*.",
        "*.*",
        ".*."
      };
      var expected3 = new []
      {
        ".A.",
        "F.B",
        ".G.",
        "E.C",
        ".D."
      };

      //WHEN
      var fullSequence = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G'};

      driver.Display(fullSequence);
      driver.Display(fullSequence);
      driver.Display(fullSequence);
      driver.Display(fullSequence);

      //THEN
      display.Received(2).Put(expected1);
      display.Received(1).Put(expected2);
      display.Received(1).Put(expected3);
      Received.InOrder(() =>
      {
        display.Put(expected1);
        display.Put(expected2);
        display.Put(expected3);
        display.Put(expected1);
      });
    }
  }
}

