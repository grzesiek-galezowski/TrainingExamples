namespace Application.ApplicationLogic;

public interface ITrackPatternsObserver
{
  void TrackLoaded(TrackDto trackDto);
  void OnTrackSelectionChanged(int trackNumber);
}