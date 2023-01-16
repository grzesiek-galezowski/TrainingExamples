using AtmaFileSystem;

namespace Application;

public interface ISelectedTb03BackupFolderProcessingStep
{
  void Activate(AbsoluteDirectoryPath folderPath);
}