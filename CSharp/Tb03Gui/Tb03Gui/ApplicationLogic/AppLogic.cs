using System.Threading.Tasks;
using AtmaFileSystem;
using MidiPlayground;

namespace Tb03Gui.ApplicationLogic;

public class AppLogic
{
  private Tb03Octave _octave = Tb03Octave.Octave4;
  private readonly Sequencer _sequencer;
  private readonly IOctaveObserver _octaveObserver;
  private readonly ISequencerPositionObserver _sequencerPositionObserver;
  private readonly Synthesizer _synthesizer;
  private readonly Patterns _patterns;
  private readonly Tracks _tracks;
  private readonly ISelectedTb03BackupFolderProcessingStep _folderProcessingChain;

  public AppLogic(
    Sequencer sequencer,
    IOctaveObserver octaveObserver,
    ISequencerPositionObserver sequencerPositionObserver, 
    Patterns patterns, 
    Tracks tracks, 
    ISelectedTb03BackupFolderProcessingStep folderProcessingChain, 
    Synthesizer synthesizer)
  {
    _sequencer = sequencer;
    _octaveObserver = octaveObserver;
    _sequencerPositionObserver = sequencerPositionObserver;
    _patterns = patterns;
    _tracks = tracks;
    _folderProcessingChain = folderProcessingChain;
    _synthesizer = synthesizer;
  }

  public void SwitchToOctave(Tb03Octave newOctave)
  {
    _octave = newOctave;
    _octaveObserver.OnOctaveChanged(newOctave);
  }

  public void PreviousSequencerPosition()
  {
    _sequencer.TryBacktrackingSequencerPosition(_sequencerPositionObserver);
  }

  public void InsertNoteIntoSequencer(Tb03Note note)
  {
    _sequencer.InsertNoteIntoSequencer(note.TransposeTo(_octave));
  }

  public void PlayCurrentPattern()
  {
    _sequencer.PlayOn(_synthesizer);
  }

  public void ActivateTb03FolderPath(AbsoluteDirectoryPath folderPath)
  {
    _folderProcessingChain.Activate(folderPath);
    _patterns.Initialize(folderPath);
    _tracks.Initialize(folderPath);
  }

  public void PatternGroupWasSelected(int patternGroupNumber) //bug enum?
  {
    _patterns.SelectPatternGroup(patternGroupNumber);
  }

  public void PatternWasSelected(int patternNumber)
  {
    _patterns.SelectPattern(patternNumber);
  }

  public void ToggleSequencerNoteAccent(int noteNumber, IParameterToggleObserver parameterToggleObserver)
  {
    _sequencer.ToggleAccent(noteNumber, parameterToggleObserver);
  }

  public void ToggleNoteSlide(int noteNumber, IParameterToggleObserver parameterToggleObserver)
  {
    _sequencer.ToggleSlide(noteNumber, parameterToggleObserver);
  }

  public void TrackWasSelected(int trackNumber)
  {
    _tracks.SelectTrack(trackNumber);
  }

  public void PlayPattern(PatternNumber patternNumber, int transpose)
  {
    _patterns.PlayPatternOutOfContext(patternNumber, transpose);
  }
}