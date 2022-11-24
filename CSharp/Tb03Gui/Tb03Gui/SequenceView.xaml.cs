using System.Windows.Controls;
using System.Windows.Media;

namespace Tb03Gui;

/// <summary>
/// Interaction logic for SequenceView.xaml
/// </summary>
public partial class SequenceView : UserControl
{
  private int _sequencerPosition;
  private readonly Label[] _sequencer;

  public SequenceView()
  {
    InitializeComponent();

    _sequencerPosition = 0;
    _sequencer = new[]
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
    MarkCurrentSequencerPosition();
  }

  public void HandleNote(Tb03Note name)
  {
    InsertNoteIntoSequencer(name);
    ForwardSequencerPosition();
  }

  public void Back()
  {
    UnmarkCurrentSequencerPosition();
    TryBacktrackingSequencerPosition();
    MarkCurrentSequencerPosition();
  }


  private void TryBacktrackingSequencerPosition()
  {
    if (_sequencerPosition > 0)
    {
      _sequencerPosition--;
    }
  }

  private void InsertNoteIntoSequencer(Tb03Note note)
  {
    _sequencer[_sequencerPosition].Content = note.Name;
  }

  private void ForwardSequencerPosition()
  {
    UnmarkCurrentSequencerPosition();
    TryAdvancingSequencerPosition();
    MarkCurrentSequencerPosition();
  }

  private void TryAdvancingSequencerPosition()
  {
    if (_sequencerPosition < _sequencer.Length - 1)
    {
      _sequencerPosition++;
    }
  }

  private void MarkCurrentSequencerPosition()
  {
    _sequencer[_sequencerPosition].Background = new SolidColorBrush(Colors.AliceBlue);
  }

  private void UnmarkCurrentSequencerPosition()
  {
    if (_sequencerPosition >= 0 && _sequencerPosition < _sequencer.Length)
    {
      _sequencer[_sequencerPosition].Background = new SolidColorBrush(Colors.LightGray);
    }
  }

}