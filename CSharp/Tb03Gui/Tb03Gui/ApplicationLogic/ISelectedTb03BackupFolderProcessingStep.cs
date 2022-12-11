using AtmaFileSystem;

namespace Tb03Gui.ApplicationLogic;

public interface ISelectedTb03BackupFolderProcessingStep
{
  void Activate(AbsoluteDirectoryPath folderPath);
}