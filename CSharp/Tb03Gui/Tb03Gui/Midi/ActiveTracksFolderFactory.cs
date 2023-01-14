using Application.ApplicationLogic;
using AtmaFileSystem;

namespace Tb03Gui.Midi;

public class ActiveTracksFolderFactory : IActiveTracksFolderFactory
{
  public ITb03TracksFolder CreateActiveTracksFolder(ITrackPatternsObserver trackPatternsObserver, AbsoluteDirectoryPath folderPath)
  {
    return new ActiveTracksFolder(folderPath, trackPatternsObserver);
  }
}