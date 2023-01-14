using AtmaFileSystem;

namespace Application.ApplicationLogic;

public class Tracks
{
  private ITb03TracksFolder _tracksFolder = new NoActiveTracksFolder();
  private int _trackNumber = TrackNavigationConstants.InitialTrack;
  private readonly ITrackPatternsObserver _trackPatternsObserver;
  private readonly IActiveTracksFolderFactory _activeTracksFolderFactory;

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

  public void SelectTrack(int trackNumber) //bug call it from outside
  {
    _trackNumber = trackNumber;
    _trackPatternsObserver.OnTrackSelectionChanged(_trackNumber);
    _tracksFolder.LoadTrack(_trackNumber);
  }
}