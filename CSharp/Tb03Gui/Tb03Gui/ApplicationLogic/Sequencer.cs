using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Maybe;
using Midi.Enums;
using MidiPlayground;

namespace Tb03Gui.ApplicationLogic;

public class Sequencer : IPatternNotesObserver
{
  private int _sequencerPosition;
  private readonly int _sequenceLength;
  private readonly ISequencerPositionObserver _positionObserver;
  private Maybe<Tb03Note>[] _notes;
  public const int InitialSequencerPosition = 0;

  public Sequencer(int sequencerLength, ISequencerPositionObserver positionObserver)
  {
    _sequencerPosition = InitialSequencerPosition;
    _sequenceLength = sequencerLength;
    _positionObserver = positionObserver;
    _notes = EmptySequence();
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

  public void InsertNoteIntoSequencer(Tb03Note latestNode)
  {
    InsertNoteAtCurrentSequencerPosition(latestNode);
    _positionObserver.OnNoteInsert(_sequencerPosition, latestNode);
    TryAdvancingSequencerPosition();
    _positionObserver.OnSequencerPositionChange(_sequencerPosition - 1, _sequencerPosition);
  }

  private void InsertNoteAtCurrentSequencerPosition(Tb03Note latestNode)
  {
    _notes[_sequencerPosition] = latestNode.Just();
  }

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
    ClearSequence();
    FillSequenceWith(steps);
  }

  private void FillSequenceWith(SequenceStepDto[] steps)
  {
    //bug support patterns with silence
    for (var i = 0; i < steps.Length; i++)
    {
      var step = steps[i];
      var note = Tb03Note.From(step.Note, 
        Convert.ToBoolean(step.Accent), 
        Convert.ToBoolean(step.Slide), 
        step.State);
      InsertNoteIntoSequencer(note);
    }
  }

  private void ClearSequence()
  {
    _notes = EmptySequence();
    _sequencerPosition = InitialSequencerPosition;
  }

  private Maybe<Tb03Note>[] EmptySequence()
  {
    return new Maybe<Tb03Note>[_sequenceLength];
  }
}
