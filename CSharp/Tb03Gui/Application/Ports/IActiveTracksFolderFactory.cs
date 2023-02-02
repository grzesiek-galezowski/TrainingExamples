using AtmaFileSystem;

namespace Application.Ports;

public interface IActiveTracksFolderFactory
{
  ITb03TracksFolder CreateActiveTracksFolder(ITrackPatternsObserver trackPatternsObserver, AbsoluteDirectoryPath folderPath);
}