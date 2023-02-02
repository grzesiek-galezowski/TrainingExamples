using Application.Ports;
using AtmaFileSystem;

namespace Application;

public class AppLogic : IAppLogic
{
  private Tb03Octave _octave = Tb03Octave.Octave4;
  private readonly Sequencer _sequencer;
  private readonly IOctaveObserver _octaveObserver;
  private readonly ISequencerPositionObserver _sequencerPositionObserver;
  private readonly ISynthesizer _synthesizer;
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
    ISynthesizer synthesizer)
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

  public async Task PlayCurrentPattern(CancellationToken cancellationToken)
  {
    await _sequencer.PlayOn(_synthesizer, cancellationToken);
  }

  public void SaveCurrentPattern(IPatternSavingObserver savingObserver)
  {
    _patterns.SaveCurrentPatternFrom(_sequencer, savingObserver);
  }

  public async Task ActivateTb03FolderPath(AbsoluteDirectoryPath folderPath, CancellationToken cancellationToken)
  {
    _folderProcessingChain.Activate(folderPath);
    await _patterns.Initialize(folderPath, cancellationToken);
    _tracks.Initialize(folderPath);
  }

  public async Task PatternGroupWasSelected(int patternGroupNumber, CancellationToken cancellationToken) //bug enum?
  {
    await _patterns.SelectPatternGroup(patternGroupNumber, cancellationToken);
  }

  public async Task PatternWasSelected(int patternNumber, CancellationToken cancellationToken)
  {
    await _patterns.SelectPattern(patternNumber, cancellationToken);
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

  public async Task PlayPattern(PatternNumber patternNumber, int transpose, CancellationToken cancellationToken)
  {
    await _patterns.PlayPatternOutOfContext(patternNumber, transpose, cancellationToken);
  }

  public async Task PlayCurrentTrack(CancellationToken cancellationToken)
  {
    await _tracks.PlayCurrentTrackOn(this, cancellationToken);
  }

  public void ChangeOutputDevice(string deviceName)
  {
    _synthesizer.ChangeOutputDevice(deviceName);
  }
}