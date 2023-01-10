using System;
using System.Windows;
using System.Windows.Controls;
using MidiPlayground;
using Tb03Gui.ApplicationLogic;

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

  private async void PlayButton_Click(object sender, RoutedEventArgs e)
  {
    try
    {
      await App.PlayPattern(PatternNumber.FromFlatNumber(Pattern.SelectedIndex + 1)); //bug add transpose
    }
    catch (Exception ex)
    {
      MessageBox.Show(ex.ToString());
    }
  }

  public AppLogic App { get; }
}