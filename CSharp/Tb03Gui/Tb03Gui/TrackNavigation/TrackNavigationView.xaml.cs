using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Documents;
using MidiPlayground;
using Tb03Gui.ApplicationLogic;
using Tb03Gui.Track;

namespace Tb03Gui.TrackNavigation;

/// <summary>
/// Interaction logic for TrackNavigationView.xaml
/// </summary>
public partial class TrackNavigationView : UserControl, ITrackPatternsObserver
{
  private List<TrackBarView> _bars = new();

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
      _bars[i].Pattern.SelectedValue = trackDto.Entries[i].Pattern;
      _bars[i].Transpose.SelectedValue = trackDto.Entries[i].Number;
    }
  }

  public void OnTrackChanged(int trackNumber)
  {
    //bug throw new NotImplementedException();
  }
}