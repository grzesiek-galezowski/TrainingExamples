using System.Collections.Immutable;
using Application.Ports;
using Core.Maybe;

namespace Application;

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

  public async Task PlayOn(ISynthesizer synthesizer, CancellationToken cancellationToken)
  {
    var pitches = _notes
      .Where(n => n.HasValue)
      .Select(n => n.Value().Pitch).ToList();
    await synthesizer.Play(pitches, cancellationToken);
  }

  public async Task PatternLoaded(SequenceDto sequence, CancellationToken cancellationToken)
  {
    ClearSequence();
    FillSequenceWith(sequence.Steps);
  }

  private void FillSequenceWith(ImmutableArray<SequenceStepDto> steps)
  {
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
    var note = _notes[noteNumber - 1];
    if (note.HasValue)
    {
      var newAccentValue = !note.Value().Accent;
      _notes[noteNumber - 1] = (note.Value() with { Accent = newAccentValue }).Just();
      parameterToggleObserver.OnAccentChanged(newAccentValue);
    }
  }

  public void ToggleSlide(int noteNumber, IParameterToggleObserver parameterToggleObserver)
  {
    var note = _notes[noteNumber - 1];
    if (note.HasValue)
    {
      var newSlideValue = !note.Value().Slide;
      _notes[noteNumber - 1] = (note.Value() with { Slide = newSlideValue }).Just();
      parameterToggleObserver.OnSlideChanged(newSlideValue);
    }
  }

  private Maybe<Tb03Note>[] CleanSequence()
  {
    return new Maybe<Tb03Note>[_sequenceLength];
  }

  public void SavePattern(
    ITb03PatternsFolder patternsFolder, 
    PatternNumber patternNumber)
  {
    patternsFolder.SavePattern(_notes, _sequenceLength, patternNumber);
  }
}

//bug support patterns with silence
//bug support resizing track
//bug support playing the entire track