using AtmaFileSystem;
using MidiPlayground;

namespace Tb03Gui.ApplicationLogic;

public class Tracks
{
  private ITb03TracksFolder _tracksFolder = new NoActiveTracksFolder();
  private int _trackNumber = TrackNavigationConstants.InitialTrack;
  private readonly ITrackPatternsObserver _trackPatternsObserver;

  public Tracks(ITrackPatternsObserver trackPatternsObserver)
  {
    _trackPatternsObserver = trackPatternsObserver;
  }

  public void Initialize(AbsoluteDirectoryPath folderPath)
  {
    _tracksFolder = new ActiveTracksFolder(folderPath, _trackPatternsObserver);
    SelectTrack(_trackNumber);
  }

  public void SelectTrack(int trackNumber) //bug call it from outside
  {
    _trackNumber = trackNumber;
    _trackPatternsObserver.OnTrackChanged(_trackNumber);
    _tracksFolder.LoadTrack(_trackNumber);
  }
}