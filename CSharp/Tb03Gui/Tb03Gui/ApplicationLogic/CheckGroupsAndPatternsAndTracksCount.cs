using System;
using System.Collections.Immutable;
using System.Linq;
using AtmaFileSystem;
using AtmaFileSystem.IO;

namespace Tb03Gui.ApplicationLogic;

public class CheckGroupsAndPatternsAndTracksCount : ISelectedTb03BackupFolderProcessingStep
{
  private const int MinGroupNumber = 1;
  private const int MaxGroupNumber = 4;
  private const int MinPatternNumber = 1;
  private const int MaxPatternNumber = 24;
  
  private const int MinTrackNumber = 1;
  private const int MaxTrackNumber = 7;

  private readonly ISelectedTb03BackupFolderProcessingStep _next;
  private readonly ITb03FolderProcessingObserver _errorObserver;

  public CheckGroupsAndPatternsAndTracksCount(ITb03FolderProcessingObserver errorObserver,
    ISelectedTb03BackupFolderProcessingStep next)
  {
    _next = next;
    _errorObserver = errorObserver;
  }

  public void Activate(AbsoluteDirectoryPath folderPath)
  {
    AssertAllPatternFilesArePresentIn(folderPath);
    AssertAllTrackFilesArePresentIn(folderPath);

    _next.Activate(folderPath);
  }

  private void AssertAllTrackFilesArePresentIn(AbsoluteDirectoryPath folderPath)
  {
    var fileNames = folderPath.GetFiles().Select(p => p.FileName()).ToImmutableArray();

    for (var trackNumber = MinTrackNumber; trackNumber <= MaxTrackNumber; trackNumber++)
    {
      var fileName = Tb03TrackFileName.For(trackNumber);
      if (!fileNames.Contains(fileName))
      {
        throw new TrackFileNotFoundException(folderPath, fileName);
      }
    }
  }

  private static void AssertAllPatternFilesArePresentIn(AbsoluteDirectoryPath folderPath)
  {
    var fileNames = folderPath.GetFiles().Select(p => p.FileName()).ToImmutableArray();

    for (var groupNumber = MinGroupNumber; groupNumber <= MaxGroupNumber; groupNumber++)
    {
      for (var patternNumber = MinPatternNumber; patternNumber <= MaxPatternNumber; patternNumber++)
      {
        var fileName = Tb03PatternFileName.For(groupNumber, patternNumber);
        if (!fileNames.Contains(fileName))
        {
          throw new PatternFileNotFoundException(folderPath, fileName);
        }
      }
    }
  }
}

internal class TrackFileNotFoundException : Exception
{
  public TrackFileNotFoundException(AbsoluteDirectoryPath folderPath, FileName fileName)
    : base($"Expected to find {fileName} track file in {folderPath} but couldn't")
  {
  }
}

internal class PatternFileNotFoundException : Exception
{
  public PatternFileNotFoundException(AbsoluteDirectoryPath folderPath, FileName fileName)
  : base($"Expected to find {fileName} pattern file in {folderPath} but couldn't")
  {
  }
}