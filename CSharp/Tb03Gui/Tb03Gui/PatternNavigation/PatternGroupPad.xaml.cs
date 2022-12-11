using System.Windows.Controls;
using System.Windows.Media;
using Tb03Gui.ApplicationLogic;

namespace Tb03Gui.PatternNavigation;

/// <summary>
/// Interaction logic for PatternGroupPad.xaml
/// </summary>
public partial class PatternGroupPad : UserControl
{
  private int _patternGroupNumber;

  public PatternGroupPad()
  {
    InitializeComponent();
  }

  public AppLogic App { get; set; }

  public int PatternGroupNumber
  {
    get => _patternGroupNumber;
    set
    {
      GroupButton.Content = value;
      _patternGroupNumber = value;
    }
  }

  private void GroupButton_Click(object sender, System.Windows.RoutedEventArgs e)
  {
    App.PatternGroupWasSelected(PatternGroupNumber);
  }

  public void Mark()
  {
    GroupButton.Background = new SolidColorBrush(Colors.AliceBlue);
  }

  public void Unmark()
  {
    GroupButton.Background = new SolidColorBrush(Colors.LightGray);
  }
}