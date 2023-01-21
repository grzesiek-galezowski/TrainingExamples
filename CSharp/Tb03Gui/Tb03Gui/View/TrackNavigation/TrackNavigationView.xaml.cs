using System;
using System.Collections.Generic;
using System.Windows.Controls;
using Application.Ports;
using Tb03Gui.View.Track;

namespace Tb03Gui.View.TrackNavigation;

/// <summary>
/// Interaction logic for TrackNavigationView.xaml
/// </summary>
public partial class TrackNavigationView : UserControl, ITrackPatternsObserver
{
  private readonly List<TrackBarView> _bars = new();
  private List<TrackPad> _trackPads = new();

  public TrackNavigationView()
  {
    InitializeComponent();
  }

  public void Initialize(IAppLogic appLogic)
  {
    App = appLogic;
    _trackPads = new List<TrackPad>
    {
      Track1Pad,
      Track2Pad,
      Track3Pad,
      Track4Pad,
      Track5Pad,
      Track6Pad,
      Track7Pad,
    };
    AddTrackSegmentsToTrackGrid();
  }

  private void AddTrackSegmentsToTrackGrid()
  {
    var maxPatternsInATrack = 256;
    var rowCount = 16;
    var patternsCountInASingleRow = maxPatternsInATrack/rowCount;

    for (int i = 0; i < patternsCountInASingleRow; i++)
    {
      TracksGrid.ColumnDefinitions.Add(new ColumnDefinition());
    }

    for (int i = 0; i < rowCount; i++)
    {
      TracksGrid.RowDefinitions.Add(new RowDefinition());
    }

    for (int i = 0; i < maxPatternsInATrack; i++)
    {
      var uiElement = new TrackBarView(App);
      Grid.SetColumn(uiElement, i % patternsCountInASingleRow);
      Grid.SetRow(uiElement, i/patternsCountInASingleRow);
      TracksGrid.Children.Add(uiElement);
      _bars.Add(uiElement);
    }
  }

  private IAppLogic App { get; set; }

  public void TrackLoaded(TrackDto trackDto)
  {
    if (trackDto.Entries.Length != _bars.Count)
    {
      throw new InvalidOperationException("PRM Bar count not the same as UI element count");
    }

    for (var i = 0; i < trackDto.Entries.Length; i++)
    {
      var currentBar = _bars[i];
      if (ExceedsDaCapoDefinedIn(trackDto, i))
      {
        currentBar.Hide();
      }
      else
      {
        currentBar.Show();
      }

      currentBar.UpdateWith(trackDto.Entries[i]);
    }
  }

  public void OnTrackSelectionChanged(int trackNumber)
  {
    foreach (var trackPad in _trackPads)
    {
      trackPad.Unmark();
    }
    _trackPads[trackNumber-1].Mark();
  }

  private static bool ExceedsDaCapoDefinedIn(TrackDto trackDto, int i)
  {
    return trackDto.Bars < i + 1;
  }

  private void PlayTrackButton_Click(object sender, System.Windows.RoutedEventArgs e)
  {
      App.PlayCurrentTrack();
  }
}