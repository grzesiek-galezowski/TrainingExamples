using MidiPlayground;

namespace Tb03Gui.ApplicationLogic;

public interface ITrackPatternsObserver
{
  void TrackLoaded(TrackEntryDto[] trackPatternsDtos);
}