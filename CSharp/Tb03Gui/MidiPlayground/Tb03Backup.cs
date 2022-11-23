using AtmaFileSystem;

namespace MidiPlayground;

public class Tb03Backup
{
  private readonly AbsoluteDirectoryPath _rootPath;

  public Tb03Backup(AbsoluteDirectoryPath absoluteDirectoryPath)
  {
    _rootPath = absoluteDirectoryPath;
  }

  public Tb03Pattern Pattern(
    Tb03PatternGroupNumber patternGroup, 
    Tb03PatternNumberInGroup number)
  {
    var prmString = PrmFile.Read(_rootPath, patternGroup, number);
    var sequenceStepDtos = PrmParser.ParseIntoPattern(prmString);
    return new Tb03Pattern(sequenceStepDtos);
  }
}