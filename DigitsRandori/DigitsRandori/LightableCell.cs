using System.Linq;

namespace DigitsRandori
{
  public class LightableCell : Cell
  {
    private readonly int _matchCode;
    private readonly Cell _defaultValue;
    private readonly Cell _valueOnMatch;

    public LightableCell(int matchCode, Cell defaultValue, Cell valueOnMatch)
    {
      _matchCode = matchCode;
      _defaultValue = defaultValue;
      _valueOnMatch = valueOnMatch;
    }

    public string LightAccordingTo(int[] ints)
    {
      if (ints.Contains(_matchCode))
      {
        return _valueOnMatch.LightAccordingTo(ints);
      }
      else
      {
        return _defaultValue.LightAccordingTo(ints);
      }
    }

    public static LightableCell Horizontal(int lightOnCode)
    {
      return new LightableCell(lightOnCode, StaleCell.Off(), new StaleCell("-"));
    }

    public static LightableCell Vertical(int lightOnCode)
    {
      return new LightableCell(lightOnCode, StaleCell.Off(), new StaleCell("|"));
    }
  }
}