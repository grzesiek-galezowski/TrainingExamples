using AtmaFileSystem;

namespace Application.Ports;

public interface IPatternsFolderFactory
{
  ITb03PatternsFolder PatternsFolder(IPatternNotesObserver sequencer, AbsoluteDirectoryPath folderPath);
}