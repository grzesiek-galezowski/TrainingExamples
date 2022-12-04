using AtmaFileSystem;
using AtmaFileSystem.IO;

namespace Tb03Gui;

public class CheckThatFolderContainsOnlyPrmFilesStep : ISelectedTb03BackupFolderProcessingStep
{
  private readonly ITb03FolderProcessingObserver _observer;
  private readonly ISelectedTb03BackupFolderProcessingStep _next;

  public CheckThatFolderContainsOnlyPrmFilesStep(ITb03FolderProcessingObserver observer, ISelectedTb03BackupFolderProcessingStep next)
  {
    _observer = observer;
    _next = next;
  }

  public void Activate(AbsoluteDirectoryPath folderPath)
  {
    foreach (var filePath in folderPath.GetFiles())
    {
      if (!filePath.Has(FileExtension.Value(".PRM")))
      {
        _observer.NotATb03File(filePath);
        return;
      }
    }
    _next.Activate(folderPath);
  }
}