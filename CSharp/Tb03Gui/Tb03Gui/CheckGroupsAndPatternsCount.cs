using System.Collections.Immutable;
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
  private readonly ITb03FolderProcessingObserver _errorObserver;

  public CheckGroupsAndPatternsCount(ITb03FolderProcessingObserver errorObserver,
    ISelectedTb03BackupFolderProcessingStep next)
  {
    _next = next;
    _errorObserver = errorObserver;
  }

  public void Handle(AbsoluteDirectoryPath folderPath)
  {
    var fileNames = folderPath.GetFiles().Select(p => p.FileName()).ToImmutableArray();
    for (var groupNumber = MinGroupNumber; groupNumber <= MaxGroupNumber; groupNumber++)
    {
      for (var patternNumber = MinPatternNumber; patternNumber <= MaxPatternNumber; patternNumber++)
      {
        var fileName = Tb03PatternFileName.For(groupNumber, patternNumber);
        if (!fileNames.Contains(fileName))
        {
          _errorObserver.PatternFileNotFound(folderPath, fileName);
          return;
        }
      }
    }
    _next.Handle(folderPath);
  }
}