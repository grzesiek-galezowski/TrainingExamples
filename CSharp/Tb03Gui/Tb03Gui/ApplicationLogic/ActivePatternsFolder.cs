using System.IO;
using AtmaFileSystem;
using MidiPlayground;

namespace Tb03Gui.ApplicationLogic;

public class ActivePatternsFolder : ITb03PatternsFolder
{
  private readonly AbsoluteDirectoryPath _folderPath;
  private readonly IPatternNotesObserver _patternNotesObserver;

  public ActivePatternsFolder(
    AbsoluteDirectoryPath folderPath,
    IPatternNotesObserver patternNotesObserver)
  {
    _folderPath = folderPath;
    _patternNotesObserver = patternNotesObserver;
  }

  public void LoadPattern(int patternGroupNumber, int patternNumberInGroup)
  {
    var patternNumber = PatternNumber.FromGroupAndNumberInGroup(patternGroupNumber, patternNumberInGroup);
    LoadPattern(patternNumber, _patternNotesObserver);
  }

  public void LoadPattern(PatternNumber patternNumber, IPatternNotesObserver patternNotesObserver)
  {
    var fileName = Tb03PatternFileName.For(_folderPath, patternNumber.PatternGroupNumber, patternNumber.PatternNumberInGroup);
    var fileContent = File.ReadAllText(fileName.ToString());
    var sequenceDto = PrmParser.ParseIntoPattern(fileContent);
    patternNotesObserver.PatternLoaded(sequenceDto);
  }
}