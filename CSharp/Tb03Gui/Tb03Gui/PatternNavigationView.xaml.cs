using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AtmaFileSystem;
using Ookii.Dialogs.Wpf;

namespace Tb03Gui;

/// <summary>
/// Interaction logic for PatternNavigationView.xaml
/// </summary>
public partial class PatternNavigationView : UserControl, ITb03FolderProcessingObserver
{
  private readonly Button[] _patternGroupPads;

  public PatternNavigationView()
  {
    InitializeComponent();
    _patternGroupPads = new[]
    {
      Group1Button,
      Group2Button,
      Group3Button,
      Group4Button
    };

    //bug _patternPads = new[]
  }

  public AppLogic App { get; set; }

  private void Button_Click(object sender, RoutedEventArgs e)
  {
    var openFolderDialog = new VistaFolderBrowserDialog();
    if (openFolderDialog.ShowDialog().GetValueOrDefault())
    {
      var folderPath = AbsoluteDirectoryPath.Value(openFolderDialog.SelectedPath);
      App.HandleTb03FolderPath(folderPath, this);
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