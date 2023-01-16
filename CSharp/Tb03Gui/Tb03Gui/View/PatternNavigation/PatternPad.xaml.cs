using System.Windows.Controls;
using System.Windows.Media;
using Application.Ports;

namespace Tb03Gui.View.PatternNavigation;

/// <summary>
/// Interaction logic for PatternGroupPad.xaml
/// </summary>
public partial class PatternPad : UserControl
{
  private int _patternNumber;

  public PatternPad()
  {
    InitializeComponent();
  }

  public IAppLogic App { get; set; }

  public int PatternNumber
  {
    get => _patternNumber;
    set
    {
      PatternButton.Content = value;
      _patternNumber = value;
    }
  }

  private void PatternButton_Click(object sender, System.Windows.RoutedEventArgs e)
  {
    App.PatternWasSelected(PatternNumber);
    Mark();
  }

  public void Mark()
  {
    PatternButton.Background = new SolidColorBrush(Colors.AliceBlue);
  }

  public void Unmark()
  {
    PatternButton.Background = new SolidColorBrush(Colors.LightGray);
  }

  private void Button_Click()
  {

  }
}