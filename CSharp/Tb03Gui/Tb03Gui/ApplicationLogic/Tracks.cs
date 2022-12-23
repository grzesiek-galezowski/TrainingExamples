using AtmaFileSystem;
using MidiPlayground;

namespace Tb03Gui.ApplicationLogic;

public class Tracks : ITrackPatternsObserver
{
  private ITb03TracksFolder _tracksFolder = new NoActiveTracksFolder();
  private int _trackNumber = TrackNavigationConstants.InitialTrack;

  public void Initialize(AbsoluteDirectoryPath folderPath)
  {
    _tracksFolder = new ActiveTracksFolder(folderPath, this); //bug pass observer
    SelectTrack(_trackNumber);
  }

  public void SelectTrack(int trackNumber) //bug call it from outside
  {
    _trackNumber = trackNumber;
    _trackPatternsObserver.OnTrackChanged(_trackNumber);
    _tracksFolder.LoadTrack(_trackNumber);
  }

  public void TrackLoaded(TrackEntryDto[] trackPatternsDtos)
  {
    throw new System.NotImplementedException();
  }
}