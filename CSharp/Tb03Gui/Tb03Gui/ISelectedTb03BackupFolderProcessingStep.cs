using AtmaFileSystem;

namespace Tb03Gui;

public interface ISelectedTb03BackupFolderProcessingStep
{
  void Handle(AbsoluteDirectoryPath folderPath);
}