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

  public async Task PlayCurrentTrackOn(AppLogic appLogic, CancellationToken cancellationToken)
  {
    await _currentTrackData.DoAsync(async track =>
    {
      for (var index = 0; 
           index < track.Bars && !cancellationToken.IsCancellationRequested; 
           index++)
      {
        var trackEntryDto = track.Entries[index];
        await appLogic.PlayPattern(
          PatternNumber.FromFlatNumber(trackEntryDto.Pattern),
          trackEntryDto.Transpose,
          cancellationToken);
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