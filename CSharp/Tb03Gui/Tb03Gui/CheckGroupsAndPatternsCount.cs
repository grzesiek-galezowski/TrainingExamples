using System.Linq;
using System.Windows;
using AtmaFileSystem;
using AtmaFileSystem.IO;

namespace Tb03Gui;

public class CheckGroupsAndPatternsCount : ISelectedTb03BackupFolderProcessingStep
{
  private const int MinGroupNumber = 1;
  private const int MaxGroupNumber = 4;
  private const int MinPatternNumber = 1;
  private const int MaxPatternNumber = 24;
  private readonly ISelectedTb03BackupFolderProcessingStep _next;

  public CheckGroupsAndPatternsCount(ISelectedTb03BackupFolderProcessingStep next)
  {
    _next = next;
  }

  public void Handle(AbsoluteDirectoryPath folderPath)
  {
    var fileNames = folderPath.GetFiles().Select(p => p.FileName());
    for (var groupNumber = MinGroupNumber; groupNumber <= MaxGroupNumber; groupNumber++)
    {
      for (var patternNumber = MinPatternNumber; patternNumber <= MaxPatternNumber; patternNumber++)
      {
        var fileName = Tb03PatternFileName.For(groupNumber, patternNumber);
        if (!fileNames.Contains(fileName))
        {
          MessageBox.Show($"Expected to find {fileName} in {folderPath} but couldn't");
          return;
        }
      }
    }
    _next.Handle(folderPath);
  }
}