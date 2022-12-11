namespace Tb03Gui.ApplicationLogic;

public interface ISequencerPositionObserver
{
  void OnSequencerPositionChange(int prevPosition, int newPosition);
  void OnNoteInsert(int sequencerPosition, Tb03Note latestNote);
}