
using TddEbook.TddToolkit.ImplementationDetails.TypeResolution.CustomCollections;

namespace LED1
{
  class CircularDisplayable : Displayable
  {
    readonly CircularList<Displayable> _displayables;

    public CircularDisplayable(params Displayable[] displayables)
    {
      _displayables = new CircularList<Displayable>(displayables);
    }

    public string Evaluate(char[] inputTriggers)
    {
      return _displayables.Next().Evaluate(inputTriggers);
    }
  }
}