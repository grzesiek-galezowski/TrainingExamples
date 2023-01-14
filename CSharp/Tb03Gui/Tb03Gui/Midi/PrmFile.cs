using System.IO;
using AtmaFileSystem;

namespace Tb03Gui.Midi;

internal static class PrmFile
{
  public static string Read(AbsoluteDirectoryPath absoluteDirectoryPath, Tb03PatternGroupNumber patternGroup, Tb03PatternNumberInGroup number)
  {
    return File.ReadAllText(
      absoluteDirectoryPath.AddFileName(
          $"TB03_PTN{(int)patternGroup}_{(int)number:D2}.PRM")
        .ToString());
  }
}