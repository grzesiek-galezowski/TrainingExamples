using System.Linq;
using System.Threading.Tasks;
using Core.Maybe;
using Midi.Enums;
using MidiPlayground;

namespace Tb03Gui;

public class Sequencer : IPatternNotesObserver
{
  private int _sequencerPosition;
  private readonly int _sequenceLength;
  private readonly Maybe<Tb03Note>[] _notes;

  public Sequencer(int sequencerLength)
  {
    _sequencerPosition = InitialSequencerPosition;
    _sequenceLength = sequencerLength;
    _notes = new Maybe<Tb03Note>[sequencerLength];
  }

  public void TryBacktrackingSequencerPosition(ISequencerPositionObserver observer)
  {
    if (_sequencerPosition > 0)
    {
      observer.OnSequencerPositionChange(_sequencerPosition, _sequencerPosition - 1);
      _sequencerPosition--;
    }
  }

  private void TryAdvancingSequencerPosition()
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
    InsertNote(latestNode);
    sequencerPositionObserver.OnNoteInsert(_sequencerPosition, latestNode);
    TryAdvancingSequencerPosition();
    sequencerPositionObserver.OnSequencerPositionChange(_sequencerPosition-1, _sequencerPosition);
  }

  private void InsertNote(Tb03Note latestNode)
  {
    _notes[_sequencerPosition] = latestNode.Just();
  }

  public const int InitialSequencerPosition = 0;

  public async Task PlayOn(Synth synth)
  {
    var pitches = _notes
      .Where(n => n.HasValue)
      .Select(n => n.Value())
      .Select(p => (Pitch)p.Pitch).ToList();
    await synth.Play(pitches);
  }

  public void PatternLoaded(SequenceStepDto[] steps)
  {
    foreach (var sequenceStepDto in steps)
    {
      
    }
  }
}