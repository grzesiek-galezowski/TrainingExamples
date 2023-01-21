using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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

  public async Task LoadPattern(int patternGroupNumber, int patternNumberInGroup, CancellationToken cancellationToken)
  {
    var patternNumber = PatternNumber.FromGroupAndNumberInGroup(patternGroupNumber, patternNumberInGroup);
    await LoadPattern(patternNumber, _patternNotesObserver, cancellationToken);
  }

  //bug fix async
  public async Task LoadPattern(PatternNumber patternNumber, IPatternNotesObserver patternNotesObserver,
    CancellationToken cancellationToken)
  {
    await LoadPattern(patternNumber, 0, patternNotesObserver, cancellationToken);
  }

  public async Task LoadPattern(PatternNumber patternNumber, int transpose, IPatternNotesObserver patternNotesObserver,
    CancellationToken cancellationToken)
  {
    var fileName = Tb03PatternFileName.For(_folderPath, patternNumber.PatternGroupNumber, patternNumber.PatternNumberInGroup);
    var fileContent = await File.ReadAllTextAsync(fileName.ToString(), cancellationToken);
    var sequenceDto = PrmParser.ParseIntoPattern(fileContent);
    await patternNotesObserver.PatternLoaded(
      new SequenceDto(Transpose(sequenceDto.Steps, transpose)), cancellationToken);
  }

  private ImmutableArray<SequenceStepDto> Transpose(ImmutableArray<SequenceStepDto> sequenceDtoSteps, int transpose)
  {
    //bug duplication. Transposition is also handled in another piece of code
    return sequenceDtoSteps.Select(s => s with { Note = s.Note + 12 * transpose}).ToImmutableArray();
  }
}