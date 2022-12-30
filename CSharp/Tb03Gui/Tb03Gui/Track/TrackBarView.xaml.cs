using System.Windows;
using System.Windows.Controls;
using MidiPlayground;

namespace Tb03Gui.Track;

/// <summary>
/// Interaction logic for TrackBarView.xaml
/// </summary>
public partial class TrackBarView : UserControl
{
  public TrackBarView()
  {
    InitializeComponent();
  }

  public void Show()
  {
    Visibility = Visibility.Visible;
  }

  public void Hide()
  {
    Visibility = Visibility.Hidden;
  }

  public void UpdateWith(TrackEntryDto trackDtoEntry)
  {
    Pattern.SelectedIndex = trackDtoEntry.Pattern;
    Transpose.SelectedIndex = trackDtoEntry.Transpose;
  }
}