using AtmaFileSystem;

namespace Application.Ports;

public interface IAppLogic
{
  void SwitchToOctave(Tb03Octave newOctave);
  void PreviousSequencerPosition();
  void InsertNoteIntoSequencer(Tb03Note note);
  Task PlayCurrentPattern(CancellationToken cancellationToken);
  Task ActivateTb03FolderPath(AbsoluteDirectoryPath folderPath, CancellationToken cancellationToken);
  Task PatternGroupWasSelected(int patternGroupNumber, CancellationToken cancellationToken);
  Task PatternWasSelected(int patternNumber, CancellationToken cancellationToken);
  void ToggleSequencerNoteAccent(int noteNumber, IParameterToggleObserver parameterToggleObserver);
  void ToggleNoteSlide(int noteNumber, IParameterToggleObserver parameterToggleObserver);
  void TrackWasSelected(int trackNumber);
  Task PlayPattern(PatternNumber patternNumber, int transpose, CancellationToken cancellationToken);
  Task PlayCurrentTrack(CancellationToken cancellationToken);
}