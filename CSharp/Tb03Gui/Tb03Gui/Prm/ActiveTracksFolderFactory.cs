using Application.Ports;
using AtmaFileSystem;

namespace Tb03Gui.Prm;

public class ActiveTracksFolderFactory : IActiveTracksFolderFactory
{
  public ITb03TracksFolder CreateActiveTracksFolder(ITrackPatternsObserver trackPatternsObserver, AbsoluteDirectoryPath folderPath)
  {
    return new ActiveTracksFolder(folderPath, trackPatternsObserver);
  }
}