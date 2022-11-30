using System.Windows.Controls;
using AtmaFileSystem;

namespace Tb03Gui;

internal class PopulateInfoStep : ISelectedTb03BackupFolderProcessingStep
{
  private readonly Button _selectTb03BackupFolderButton;

  public PopulateInfoStep(Button selectTb03BackupFolderButton)
  {
    _selectTb03BackupFolderButton = selectTb03BackupFolderButton;
  }

  public void Handle(AbsoluteDirectoryPath folderPath)
  {
    _selectTb03BackupFolderButton.Content = folderPath.ToString();
  }
}