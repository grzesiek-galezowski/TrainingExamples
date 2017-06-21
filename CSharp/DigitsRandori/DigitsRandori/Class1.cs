using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

//TODO exercises
//1. Add support for more rows
//2. display with memory - you can "add" to lightened and "put out" some additional lights
//3. Add support to cells that are toggleable, e.g. first time show "|" or "-", second time they show "*", third time they show "." (dark)


namespace DigitsRandori
{
  public class Class1
  {
    [Fact]
    public void ShouldLighNoBulbsWhenEmptyArrayIsProvided()
    {
      //GIVEN
      var realDisplay = Substitute.For<Display>();
      var driver = CompositionRoot.CreateDriver(realDisplay);

      //WHEN
      driver.Send();

      //THEN
      realDisplay.Received(1).Put(
        "...",
        "...",
        "...",
        "...",
        "...");
    }

    [Fact]
    public void ShouldLightOneBulbsWhenIndex0IsProvided()
    {
      //GIVEN
      var realDisplay = Substitute.For<Display>();
      var driver = CompositionRoot.CreateDriver(realDisplay);

      //WHEN
      driver.Send(0);

      //THEN
      realDisplay.Received(1).Put(
        ".-.",
        "...",
        "...",
        "...",
        "...");
    }

    [Fact]
    public void ShouldLightSecondBulbWhenIndex1IsProvided()
    {
      //GIVEN
      var realDisplay = Substitute.For<Display>();
      var driver = CompositionRoot.CreateDriver(realDisplay);

      //WHEN
      driver.Send(1);

      //THEN
      realDisplay.Received(1).Put(
        "...",
        "|..",
        "...",
        "...",
        "...");
    }

    [Fact]
    public void ShouldLightThirdBulbWhenIndex2IsProvided()
    {
      //GIVEN
      var realDisplay = Substitute.For<Display>();
      var driver = CompositionRoot.CreateDriver(realDisplay);

      //WHEN
      driver.Send(2);

      //THEN
      realDisplay.Received(1).Put(
        "...",
        "..|",
        "...",
        "...",
        "...");
    }

    [Fact]
    public void ShouldLightAllBulbsWhenAll7IndexesAreProvided()
    {
      //GIVEN
      var realDisplay = Substitute.For<Display>();
      var driver = CompositionRoot.CreateDriver(realDisplay);

      //WHEN
      driver.Send(0,1,2,3,4,5,6);

      //THEN
      realDisplay.Received(1).Put(
        ".-.",
        "|.|",
        ".-.",
        "|.|",
        ".-.");
    }


  }

  public class StaleCell : Cell
  {
    private readonly string _cellValue;

    public StaleCell(string cellValue)
    {
      _cellValue = cellValue;
    }

    public string LightAccordingTo(int[] ints)
    {
      return _cellValue;
    }

    public static Cell Off()
    {
      return new StaleCell(".");
    }
  }
}
