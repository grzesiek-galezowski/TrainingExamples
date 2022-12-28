using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using MidiPlayground;
using Tb03Gui.ApplicationLogic;
using Tb03Gui.Track;

namespace Tb03Gui.TrackNavigation;

/// <summary>
/// Interaction logic for TrackNavigationView.xaml
/// </summary>
public partial class TrackNavigationView : UserControl, ITrackPatternsObserver
{
  private readonly List<TrackBarView> _bars = new();

  public TrackNavigationView()
  {
    InitializeComponent();
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
      var uiElement = new TrackBarView();
      Grid.SetColumn(uiElement, i % patternsCountInASingleRow);
      Grid.SetRow(uiElement, i/patternsCountInASingleRow);
      TracksGrid.Children.Add(uiElement);
      _bars.Add(uiElement);
    }
  }

  public void TrackLoaded(TrackDto trackDto)
  {
    if (trackDto.Entries.Length != _bars.Count)
    {
      throw new InvalidOperationException("PRM Bar count not the same as UI element count");
    }

    for (var i = 0; i < trackDto.Entries.Length; i++)
    {
      var currentBar = _bars[i];
      if (ExceedsDaCapo(i, trackDto))
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
    //bug throw new NotImplementedException();
  }

  private static bool ExceedsDaCapo(int i, TrackDto trackDto)
  {
    return trackDto.Bars < i + 1;
  }
}