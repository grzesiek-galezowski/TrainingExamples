using AtmaFileSystem;
using MidiPlayground;

internal static class PrmFile
{
  public static string Read(AbsoluteDirectoryPath absoluteDirectoryPath, Tb03PatternGroupNumber patternGroup, Tb03PatternNumberInGroup number)
  {
    return File.ReadAllText(
      absoluteDirectoryPath.AddFileName(
          $"TB03_PTN{(int)patternGroup}_{((int)number).ToString("D2")}.PRM")
        .ToString());
  }
}