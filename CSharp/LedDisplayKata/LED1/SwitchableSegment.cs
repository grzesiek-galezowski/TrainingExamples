using System.Linq;

namespace LED1
{
  public class SwitchableSegment : Displayable
  {
    private readonly char _onTrigger;
    private readonly string _onValue;
    private readonly Displayable _fallbackDisplayable;

    public SwitchableSegment(char onTrigger, string onValue, Displayable fallbackDisplayable)
    {
      _onTrigger = onTrigger;
      _onValue = onValue;
      _fallbackDisplayable = fallbackDisplayable;
    }

    public string Evaluate(char[] inputTriggers)
    {
      if (inputTriggers.Contains(_onTrigger))
      {
        return _onValue;
      }
      else
      {
        return _fallbackDisplayable.Evaluate(inputTriggers);
      }
    }
  }
}