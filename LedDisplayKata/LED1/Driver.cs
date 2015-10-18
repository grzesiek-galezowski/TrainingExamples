using System.Linq;

namespace LED1
{
  public class Driver
  {
    private readonly Display _display;
    private readonly Displayable[] _rows;

    public Driver(Display display, params Displayable[] rows)
    {
      _display = display;
      _rows = rows;
    }

    public void Display(char[] inputTriggers)
    {
      var rows = _rows.Select(r => r.Evaluate(inputTriggers)).ToArray();
      _display.Put(rows);
    }
  }
}