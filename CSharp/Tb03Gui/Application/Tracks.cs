using Application.Ports;
using AtmaFileSystem;
using Core.Maybe;

namespace Application;

public class Tracks : ITrackPatternsObserver
{
  private ITb03TracksFolder _tracksFolder = new NoActiveTracksFolder();
  private int _trackNumber = TrackNavigationConstants.InitialTrack;
  private readonly ITrackPatternsObserver _trackPatternsObserver;
  private readonly IActiveTracksFolderFactory _activeTracksFolderFactory;
  private Maybe<TrackDto> _currentTrackData;

  public Tracks(
    ITrackPatternsObserver trackPatternsObserver,
    IActiveTracksFolderFactory activeTracksFolderFactory)
  {
    _trackPatternsObserver = trackPatternsObserver;
    _activeTracksFolderFactory = activeTracksFolderFactory;
  }

  public void Initialize(AbsoluteDirectoryPath folderPath)
  {
    _tracksFolder = _activeTracksFolderFactory.CreateActiveTracksFolder(_trackPatternsObserver, folderPath);
    SelectTrack(_trackNumber);
  }

  public void SelectTrack(int trackNumber)
  {
    _trackNumber = trackNumber;
    _trackPatternsObserver.OnTrackSelectionChanged(_trackNumber);
    _tracksFolder.LoadTrack(_trackNumber, this);
  }

  public void PlayCurrentTrackOn(AppLogic appLogic)
  {
    _currentTrackData.Do(track =>
    {
      for (var index = 0; index < track.Bars; index++)
      {
        var trackEntryDto = track.Entries[index];
        appLogic.PlayPattern(
          PatternNumber.FromFlatNumber(trackEntryDto.Pattern),
          trackEntryDto.Transpose);
      }
    });
  }

  public void TrackLoaded(TrackDto trackDto)
  {
    _currentTrackData = trackDto.Just();
  }

  public void OnTrackSelectionChanged(int trackNumber)
  {
    //unused (maybe don't use this interface then?)
  }
}