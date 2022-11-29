namespace Tb03Gui;

public interface ISequencerPositionObserver
{
  void OnSequencerPositionChange(int prevPosition, int newPosition);
  void OnNoteInsert(int sequencerPosition, Tb03Note latestNote);
}