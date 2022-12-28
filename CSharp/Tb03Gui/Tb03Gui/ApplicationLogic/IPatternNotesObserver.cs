using MidiPlayground;

namespace Tb03Gui.ApplicationLogic;

public interface IPatternNotesObserver
{
  void PatternLoaded(SequenceDto sequence);
}