using System.Windows;
using System.Windows.Controls;
using AtmaFileSystem;
using AtmaFileSystem.IO;

namespace Tb03Gui;

public class CheckThatFolderContainsOnlyPrmFilesStep : ISelectedTb03BackupFolderProcessingStep
{
  private readonly ISelectedTb03BackupFolderProcessingStep _next;

  public CheckThatFolderContainsOnlyPrmFilesStep(
    ISelectedTb03BackupFolderProcessingStep next)
  {
    _next = next;
  }

  public void Handle(AbsoluteDirectoryPath folderPath)
  {
    foreach (var fileSystemEntry in folderPath.GetFiles())
    {
      if (!fileSystemEntry.Has(FileExtension.Value(".PRM")))
      {
        MessageBox.Show("The file " + fileSystemEntry.FileName() + " is not a proper PRM file");
        return;
      }
    }
    _next.Handle(folderPath);
  }
}