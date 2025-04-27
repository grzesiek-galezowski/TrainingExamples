using System.Windows.Controls;
using System.Windows.Media;
using Application.Ports;

namespace Tb03Gui.View;

public class Tb03Key
{
  private readonly Button _button;
  private readonly Tb03Note _tb03Note;

  public Tb03Key(Button button, Tb03Note tb03Note)
  {
    _button = button;
    _tb03Note = tb03Note;
  }

  public void Mark()
  {
    _button.Background = new SolidColorBrush(Colors.DarkGray);
  }

  public Tb03Note GetNote()
  {
    return _tb03Note;
  }

  public void UnMark()
  {
    _button.Background = new SolidColorBrush(Colors.LightGray);
  }
}