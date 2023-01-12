using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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
    _notes = CleanSequence();
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

  public void PlayOn(Synthesizer synthesizer)
  {
    var pitches = _notes
      .Where(n => n.HasValue)
      .Select(n => n.Value())
      .Select(p => (Pitch)p.Pitch).ToList();
    synthesizer.Play(pitches);
  }

  public void PatternLoaded(SequenceDto sequence)
  {
    ClearSequence();
    FillSequenceWith(sequence.Steps);
  }

  private void FillSequenceWith(ImmutableArray<SequenceStepDto> steps)
  {
    //bug support patterns with silence
    foreach (var note in Tb03Note.NotesFrom(steps))
    {
      InsertNoteIntoSequencer(note);
    }
  }

  private void ClearSequence()
  {
    _notes = CleanSequence();
    _sequencerPosition = InitialSequencerPosition;
  }

  public void ToggleAccent(int noteNumber, IParameterToggleObserver parameterToggleObserver)
  {
    var note = _notes[noteNumber-1];
    if (note.HasValue)
    {
      var newAccentValue = !note.Value().Accent;
      _notes[noteNumber-1] = (note.Value() with { Accent = newAccentValue}).Just();
      parameterToggleObserver.OnAccentChanged(newAccentValue);
    }
  }

  public void ToggleSlide(int noteNumber, IParameterToggleObserver parameterToggleObserver)
  {
    var note = _notes[noteNumber-1];
    if (note.HasValue)
    {
      var newSlideValue = !note.Value().Slide;
      _notes[noteNumber-1] = (note.Value() with { Slide = newSlideValue}).Just();
      parameterToggleObserver.OnSlideChanged(newSlideValue);
    }
  }

  private Maybe<Tb03Note>[] CleanSequence()
  {
    return new Maybe<Tb03Note>[_sequenceLength];
  }
}
