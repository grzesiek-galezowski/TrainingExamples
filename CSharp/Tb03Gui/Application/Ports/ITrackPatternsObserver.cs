namespace Application.Ports;

public interface ITrackPatternsObserver
{
  void TrackLoaded(TrackDto trackDto);
  void OnTrackSelectionChanged(int trackNumber);
}