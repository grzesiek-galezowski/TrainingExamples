using AtmaFileSystem;

namespace Application.ApplicationLogic;

public interface IActiveTracksFolderFactory
{
  ITb03TracksFolder CreateActiveTracksFolder(ITrackPatternsObserver trackPatternsObserver, AbsoluteDirectoryPath folderPath);
}