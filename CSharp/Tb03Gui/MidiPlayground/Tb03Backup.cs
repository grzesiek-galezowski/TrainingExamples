using AtmaFileSystem;

namespace MidiPlayground;

public class Tb03Backup
{
  public Tb03Backup(AbsoluteDirectoryPath absoluteDirectoryPath)
  {
    RootPath = absoluteDirectoryPath;
  }

  private AbsoluteDirectoryPath RootPath { get; }

  public PrmPattern Pattern(
    Tb03PatternGroupNumber patternGroup, 
    Tb03PatternNumberInGroup number)
  {
    return new PrmPattern(
      File.ReadAllText(
        RootPath.AddFileName(
            $"TB03_PTN{(int)patternGroup}_{((int)number).ToString("D2")}.PRM")
          .ToString()));
  }
}