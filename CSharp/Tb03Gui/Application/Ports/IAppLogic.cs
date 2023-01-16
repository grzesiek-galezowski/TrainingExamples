using AtmaFileSystem;

namespace Application.Ports;

public interface IAppLogic
{
  void SwitchToOctave(Tb03Octave newOctave);
  void PreviousSequencerPosition();
  void InsertNoteIntoSequencer(Tb03Note note);
  void PlayCurrentPattern();
  void ActivateTb03FolderPath(AbsoluteDirectoryPath folderPath);
  void PatternGroupWasSelected(int patternGroupNumber);
  void PatternWasSelected(int patternNumber);
  void ToggleSequencerNoteAccent(int noteNumber, IParameterToggleObserver parameterToggleObserver);
  void ToggleNoteSlide(int noteNumber, IParameterToggleObserver parameterToggleObserver);
  void TrackWasSelected(int trackNumber);
  void PlayPattern(PatternNumber patternNumber, int transpose);
}