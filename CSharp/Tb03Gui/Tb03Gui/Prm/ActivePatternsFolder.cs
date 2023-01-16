using System.Collections.Immutable;
using System.IO;
using System.Linq;
using Application;
using Application.Ports;
using AtmaFileSystem;

namespace Tb03Gui.Prm;

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

  //bug needed?
  public void LoadPattern(PatternNumber patternNumber, IPatternNotesObserver patternNotesObserver)
  {
    LoadPattern(patternNumber, 0, patternNotesObserver);
  }

  public void LoadPattern(PatternNumber patternNumber, int transpose, IPatternNotesObserver patternNotesObserver)
  {
    var fileName = Tb03PatternFileName.For(_folderPath, patternNumber.PatternGroupNumber, patternNumber.PatternNumberInGroup);
    var fileContent = File.ReadAllText(fileName.ToString());
    var sequenceDto = PrmParser.ParseIntoPattern(fileContent);
    patternNotesObserver.PatternLoaded(sequenceDto with { Steps = Transpose(sequenceDto.Steps, transpose)});
  }

  private ImmutableArray<SequenceStepDto> Transpose(ImmutableArray<SequenceStepDto> sequenceDtoSteps, int transpose)
  {
    //bug duplication. Transposition is also handled in another piece of code
    return sequenceDtoSteps.Select(s => s with { Note = s.Note + 12 * transpose}).ToImmutableArray();
  }
}