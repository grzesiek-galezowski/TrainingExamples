using System;
using System.Windows;
using System.Windows.Controls;
using Application.ApplicationLogic;

namespace Tb03Gui.Sequencer;

/// <summary>
/// Interaction logic for SequenceView.xaml
/// </summary>
public partial class SequenceView : UserControl, ISequencerPositionObserver
{
  private readonly SequencerPad[] _sequencerPads;

  public SequenceView()
  {
    InitializeComponent();
    _sequencerPads = new[]
    {
      P1,
      P2,
      P3,
      P4,
      P5,
      P6,
      P7,
      P8,
      P9,
      P10,
      P11,
      P12,
      P13,
      P14,
      P15,
      P16,
    };
    _sequencerPads[Application.ApplicationLogic.Sequencer.InitialSequencerPosition].Mark();
  }

  public AppLogic App { get; set; }

  private void PlayPause_Click(object sender, RoutedEventArgs e)
  {
    try
    {
      App.PlayCurrentPattern();
    }
    catch (Exception exception)
    {
      MessageBox.Show(exception.ToString());
    }
  }

  public void OnSequencerPositionChange(int prevPosition, int newPosition)
  {
    _sequencerPads[prevPosition].Unmark();
    _sequencerPads[newPosition].Mark();
  }

  public void OnNoteInsert(int sequencerPosition, Tb03Note latestNote)
  {
    _sequencerPads[sequencerPosition].SetNote(latestNote);
  }

  public int SequencerPatternLength()
  {
    return _sequencerPads.Length;
  }
}
