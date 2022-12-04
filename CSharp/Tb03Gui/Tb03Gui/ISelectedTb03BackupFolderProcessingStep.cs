using AtmaFileSystem;

namespace Tb03Gui;

public interface ISelectedTb03BackupFolderProcessingStep
{
  void Activate(AbsoluteDirectoryPath folderPath);
}