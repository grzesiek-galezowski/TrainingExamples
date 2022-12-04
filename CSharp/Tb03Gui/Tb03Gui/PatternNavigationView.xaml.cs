using System.Windows;
using System.Windows.Controls;
using AtmaFileSystem;
using Ookii.Dialogs.Wpf;

namespace Tb03Gui;

/// <summary>
/// Interaction logic for PatternNavigationView.xaml
/// </summary>
public partial class PatternNavigationView : UserControl, ITb03FolderProcessingObserver, IPatternNavigationObserver
{
  private readonly PatternGroupPad[] _patternGroupPads;
  private readonly PatternPad[] _patternPads;

  public PatternNavigationView()
  {
    InitializeComponent();
    _patternGroupPads = new[]
    {
      Group1Pad,
      Group2Pad,
      Group3Pad,
      Group4Pad
    };

    _patternPads = new[]
    {
      Pattern1Pad,
      Pattern2Pad,
      Pattern3Pad,
      Pattern4Pad,
      Pattern5Pad,
      Pattern6Pad,
      Pattern7Pad,
      Pattern8Pad,
      Pattern9Pad,
      Pattern10Pad,
      Pattern11Pad,
      Pattern12Pad,
      Pattern13Pad,
      Pattern14Pad,
      Pattern15Pad,
      Pattern16Pad,
      Pattern17Pad,
      Pattern18Pad,
      Pattern19Pad,
      Pattern20Pad,
      Pattern21Pad,
      Pattern22Pad,
      Pattern23Pad,
      Pattern24Pad
    };

    _patternGroupPads[PatternNavigationConstants.InitialPatternGroup-1].Mark();
    _patternPads[PatternNavigationConstants.InitialPattern-1].Mark();
  }

  public AppLogic App { get; set; }

  private void Button_Click(object sender, RoutedEventArgs e)
  {
    var openFolderDialog = new VistaFolderBrowserDialog();
    if (openFolderDialog.ShowDialog().GetValueOrDefault())
    {
      var folderPath = AbsoluteDirectoryPath.Value(openFolderDialog.SelectedPath);
      App.ActivateTb03FolderPath(folderPath, this);
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

  public void OnPatternGroupSelectionChanged(int patternGroupNumber)
  {
    foreach (var patternGroupPad in _patternGroupPads)
    {
      patternGroupPad.Unmark();
    }
  }

  public void OnPatternSelectionChanged(int patternNumber)
  {
    foreach (var patternPad in _patternPads)
    {
      patternPad.Unmark();
    }

  }
}