using System.Windows.Controls;
using System.Windows.Media;
using Tb03Gui.ApplicationLogic;

namespace Tb03Gui.TrackNavigation;

/// <summary>
/// Interaction logic for PatternGroupPad.xaml
/// </summary>
public partial class TrackPad : UserControl
{
  private int _trackNumber;

  public TrackPad()
  {
    InitializeComponent();
  }

  public AppLogic App { get; set; }

  public int TrackNumber
  {
    get => _trackNumber;
    set
    {
      PatternButton.Content = value;
      _trackNumber = value;
    }
  }

  private void PatternButton_Click(object sender, System.Windows.RoutedEventArgs e)
  {
    App.TrackWasSelected(TrackNumber);
  }

  public void Mark()
  {
    PatternButton.Background = new SolidColorBrush(Colors.AliceBlue);
  }

  public void Unmark()
  {
    PatternButton.Background = new SolidColorBrush(Colors.LightGray);
  }
}