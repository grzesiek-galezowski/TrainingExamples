using MidiPlayground;

namespace Tb03Gui.ApplicationLogic;

public interface ITrackPatternsObserver
{
  void TrackLoaded(TrackDto trackDto);
  void OnTrackChanged(int trackNumber);
}