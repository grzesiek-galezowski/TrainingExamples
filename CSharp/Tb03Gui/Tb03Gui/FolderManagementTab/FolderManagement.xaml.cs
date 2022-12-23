using System;
using System.Windows;
using System.Windows.Controls;
using AtmaFileSystem;
using Ookii.Dialogs.Wpf;
using Tb03Gui.ApplicationLogic;

namespace Tb03Gui.FolderManagementTab;

/// <summary>
/// Interaction logic for FolderManagement.xaml
/// </summary>
public partial class FolderManagement : UserControl, ITb03FolderProcessingObserver
{
  public FolderManagement()
  {
    InitializeComponent();
  }

  public AppLogic App { get; set; }

  private void Button_Click(object sender, RoutedEventArgs e)
  {
    try
    {
      var openFolderDialog = new VistaFolderBrowserDialog();
      if (openFolderDialog.ShowDialog().GetValueOrDefault())
      {
        var folderPath = AbsoluteDirectoryPath.Value(openFolderDialog.SelectedPath);
        App.ActivateTb03FolderPath(folderPath);
      }
    }
    catch (Exception ex)
    {
      MessageBox.Show(ex.Message);
    }
  }

  public void PatternFileNotFound(AbsoluteDirectoryPath folderPath, FileName fileName)
  {
    MessageBox.Show($"Expected to find {fileName} in {folderPath} but couldn't");
  }

  public void NotATb03File(AbsoluteFilePath filePath)
  {
    MessageBox.Show("The file " + filePath.FileName() + " is not a proper PRM file");
  }

  public void PathIsOk(AbsoluteDirectoryPath folderPath)
  {
    SelectTb03BackupFolderButton.Content = folderPath.ToString();
  }
}