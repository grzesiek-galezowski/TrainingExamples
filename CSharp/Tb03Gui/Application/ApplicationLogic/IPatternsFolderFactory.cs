using AtmaFileSystem;

namespace Application.ApplicationLogic;

public interface IPatternsFolderFactory
{
  ITb03PatternsFolder PatternsFolder(IPatternNotesObserver sequencer, AbsoluteDirectoryPath folderPath);
}