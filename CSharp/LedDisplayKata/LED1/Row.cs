using System.Linq;
using LED1;

public class Row : Displayable
{
  private readonly Displayable[] _displayables;

  public Row(params Displayable[] displayables)
  {
    _displayables = displayables;
  }

  public string Evaluate(char[] inputTriggers)
  {
    return _displayables.Aggregate("", (s, displayable) => s+ displayable.Evaluate(inputTriggers));
  }
}