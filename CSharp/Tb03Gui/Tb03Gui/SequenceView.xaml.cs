using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tb03Gui;

/// <summary>
/// Interaction logic for SequenceView.xaml
/// </summary>
public partial class SequenceView : UserControl, ISequencerPositionObserver
{
  private readonly Label[] _sequencerPads; //bug

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
    MarkSequencerPosition(Sequencer.InitialSequencerPosition);
  }

  public AppLogic App { get; set; }

  private void MarkSequencerPosition(int sequencerPosition)
  {
    _sequencerPads[sequencerPosition].Background = new SolidColorBrush(Colors.AliceBlue);
  }

  private void UnmarkSequencerPosition(int position)
  {
    _sequencerPads[position].Background = new SolidColorBrush(Colors.LightGray);
  }

  private async void PlayPause_Click(object sender, RoutedEventArgs e)
  {
    try
    {
      await App.Play();
    }
    catch (Exception exception)
    {
      MessageBox.Show(exception.ToString());
    }
  }

  public void OnSequencerPositionChange(int prevPosition, int newPosition)
  {
    UnmarkSequencerPosition(prevPosition);
    MarkSequencerPosition(newPosition);
  }

  public void OnNoteInsert(int sequencerPosition, Tb03Note latestNote)
  {
    _sequencerPads[sequencerPosition].Content = latestNote;
  }

  public int SequencerPatternLength()
  {
    return _sequencerPads.Length;
  }
}
