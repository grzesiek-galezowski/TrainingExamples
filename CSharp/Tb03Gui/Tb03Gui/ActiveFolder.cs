using System.IO;
using AtmaFileSystem;
using MidiPlayground;

namespace Tb03Gui;

public class ActiveFolder : ITb03Folder
{
  private readonly AbsoluteDirectoryPath _folderPath;
  private readonly IPatternNotesObserver _patternNotesObserver;

  public ActiveFolder(
    AbsoluteDirectoryPath folderPath, 
    IPatternNotesObserver patternNotesObserver)
  {
    _folderPath = folderPath;
    _patternNotesObserver = patternNotesObserver;
  }

  public void Load(int patternGroupNumber, int patternNumber)
  {
    var fileName = Tb03PatternFileName.For(_folderPath, patternGroupNumber, patternNumber);
    var fileContent = File.ReadAllText(fileName.ToString());
    var sequenceStepDtos = PrmParser.ParseIntoPattern(fileContent);
    _patternNotesObserver.PatternLoaded(sequenceStepDtos);
  }
}