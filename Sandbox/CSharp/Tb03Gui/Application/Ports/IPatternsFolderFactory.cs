using AtmaFileSystem;

namespace Application.Ports;

public interface IPatternsFolderFactory
{
  ITb03PatternsFolder PatternsFolder(IPatternLoadingObserver sequencer, AbsoluteDirectoryPath folderPath);
}