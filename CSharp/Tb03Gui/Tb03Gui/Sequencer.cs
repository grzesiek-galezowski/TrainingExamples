using System.Linq;
using System.Threading.Tasks;
using Midi.Enums;
using MidiPlayground;

namespace Tb03Gui;

public class Sequencer
{
  private readonly SequenceView _view;
  private int _sequencerPosition; //bug make private
  private readonly int _sequenceLength;
  private readonly Tb03Note[] _notes;

  public Sequencer(SequenceView view, int sequencerLength)
  {
    _sequencerPosition = InitialSequencerPosition;
    _view = view;
    _sequenceLength = sequencerLength;
    _notes = new Tb03Note[sequencerLength];
  }

  public void TryBacktrackingSequencerPosition(ISequencerPositionObserver observer)
  {
    if (_sequencerPosition > 0)
    {
      observer.OnSequencerPositionChange(_sequencerPosition, _sequencerPosition - 1);
      _sequencerPosition--;
    }
  }

  public void TryAdvancingSequencerPosition()
  {
    if (_sequencerPosition < _sequenceLength - 1)
    {
      _sequencerPosition++;
    }
  }

  public void InsertNoteIntoSequencer(
    Tb03Note note, 
    ISequencerPositionObserver sequencerPositionObserver,
    Tb03Octave currentOctave)
  {
    var latestNode = note.TransposeTo(currentOctave);
    _notes[_sequencerPosition] = latestNode;
    sequencerPositionObserver.OnNoteInsert(_sequencerPosition, latestNode);
    TryAdvancingSequencerPosition();
    sequencerPositionObserver.OnSequencerPositionChange(_sequencerPosition-1, _sequencerPosition);
  }

  public const int InitialSequencerPosition = 0;

  public async Task PlayOn(Synth synth)
  {
    var pitches = _notes
      .Select(p => (Pitch)p.Pitch).ToList();
    await synth.Play(pitches);
  }
}