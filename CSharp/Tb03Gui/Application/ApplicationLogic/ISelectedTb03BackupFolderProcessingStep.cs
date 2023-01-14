using AtmaFileSystem;

namespace Application.ApplicationLogic;

public interface ISelectedTb03BackupFolderProcessingStep
{
  void Activate(AbsoluteDirectoryPath folderPath);
}