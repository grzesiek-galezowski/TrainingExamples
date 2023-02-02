using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using Application.Ports;
using Core.Maybe;
using Core.NullableReferenceTypesExtensions;

namespace Tb03Gui.View.Track;

/// <summary>
/// Interaction logic for TrackBarView.xaml
/// </summary>
public partial class TrackBarView : UserControl
{
  private readonly IAppLogic _app;

  public TrackBarView(IAppLogic appLogic)
  {
    _app = appLogic;
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
    var patternNumber = PatternNumber.FromFlatNumber(trackDtoEntry.Pattern);
    Pattern.SelectedIndex = patternNumber.PatternNumberInGroup - 1;
    PatternGroup.SelectedIndex = patternNumber.PatternGroupNumber - 1;
    Transpose.SelectedIndex = trackDtoEntry.Transpose;
  }

  private async void PlayButton_Click(object sender, RoutedEventArgs e)
  {
    try
    {
        await _app.PlayPattern(
          PatternNumber.FromGroupAndNumberInGroup(
            SelectedIntFrom(PatternGroup),
            SelectedIntFrom(Pattern)),
          SelectedIntFrom(Transpose),
          new CancellationToken()
        );
    }
    catch (Exception ex)
    {
      MessageBox.Show(ex.ToString());
    }
  }

  private int SelectedIntFrom(ComboBox patternGroup)
  {
    return int.Parse(((ComboBoxItem)patternGroup.SelectedValue).Content.ToString().OrThrow());
  }
}