using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Midi.Enums;
using MidiPlayground;

namespace Tb03Gui;

/// <summary>
/// Interaction logic for SequenceView.xaml
/// </summary>
public partial class SequenceView : UserControl
{
  private int _sequencerPosition;
  private readonly Label[] _sequencerPads;
  private readonly Synth _synth;
  private readonly Dictionary<string, int> _midiCodeByNote;
  private int _octave;

  public SequenceView()
  {
    _synth = Synth.Create();
    InitializeComponent();
    _octave = 4;
    _midiCodeByNote = new Dictionary<string, int>
    {
      { "C", 0 },
      { "C#", 1 },
      { "D", 2 },
      { "D#", 3 },
      { "E", 4 },
      { "F", 5 },
      { "F#", 6 },
      { "G", 7 },
      { "G#", 8 },
      { "A", 9 },
      { "A#", 10 },
      { "B", 11 },
    };

    _sequencerPosition = 0;
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
    _sequencerPads[_sequencerPosition].Content = note.Name;
  }

  private void ForwardSequencerPosition()
  {
    UnmarkCurrentSequencerPosition();
    TryAdvancingSequencerPosition();
    MarkCurrentSequencerPosition();
  }

  private void TryAdvancingSequencerPosition()
  {
    if (_sequencerPosition < _sequencerPads.Length - 1)
    {
      _sequencerPosition++;
    }
  }

  private void MarkCurrentSequencerPosition()
  {
    _sequencerPads[_sequencerPosition].Background = new SolidColorBrush(Colors.AliceBlue);
  }

  private void UnmarkCurrentSequencerPosition()
  {
    if (_sequencerPosition >= 0 && _sequencerPosition < _sequencerPads.Length)
    {
      _sequencerPads[_sequencerPosition].Background = new SolidColorBrush(Colors.LightGray);
    }
  }

  private async void PlayPause_Click(object sender, RoutedEventArgs e)
  {
    try
    {
      var pitches = _sequencerPads
        .Where(x => x.Content is string s && s != string.Empty)
        .Select(x => x.Content.ToString())
        .Select(c => _midiCodeByNote[c] + _octave*12)
        .Select(p => (Pitch)p).ToList(); //bug maybe put the pitches directly
      await _synth.Play(pitches);
    }
    catch (Exception exception)
    {
      MessageBox.Show(exception.ToString());
    }
  }

  public void Octave(int newOctave)
  {
    _octave = newOctave;
  }
}