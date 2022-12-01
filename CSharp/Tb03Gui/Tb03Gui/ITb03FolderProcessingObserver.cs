using AtmaFileSystem;

namespace Tb03Gui;

public interface ITb03FolderProcessingObserver
{
  void PatternFileNotFound(AbsoluteDirectoryPath folderPath, FileName fileName);
  void NotATb03File(AbsoluteFilePath filePath);
  void PathIsOk(AbsoluteDirectoryPath folderPath);
}