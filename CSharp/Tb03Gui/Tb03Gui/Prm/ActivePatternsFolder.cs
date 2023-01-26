using System;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application;
using Application.Ports;
using AtmaFileSystem;
using Core.Maybe;

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
    var fileName = FilePathFor(patternNumber);
    var fileContent = await File.ReadAllTextAsync(fileName.ToString(), cancellationToken);
    var sequenceDto = PrmConvert.ParseIntoPattern(fileContent);
    await patternNotesObserver.PatternLoaded(
      sequenceDto with
      {
        Steps = Transpose(sequenceDto.Steps, transpose)
      },
      cancellationToken);
  }


  public void SavePattern(Maybe<Tb03Note>[] notes, int sequenceLength, PatternNumber patternNumber)
  {
    //bug somehow pass end note and triplet
    //bug the "test" extension is to avoid overriding accidentally the original files.
    var filePath = FilePathFor(patternNumber).ChangeExtensionTo(FileExtension.Value(".test"));
    var contents = PrmConvert.ParseIntoString(
      notes.Select((n, i) => new SequenceStepDto
      {
        Accent = Convert.ToInt32(n.Value().Accent),
        Note = n.Value().Pitch,
        Slide = Convert.ToInt32(n.Value().Slide),
        State = n.Value().State,
        StepNumber = i + 1
      }).ToImmutableArray(), 
      15 /* bug for now using a constant end step index */, 
      0 /* bug for now constant triplets param value */);
    File.WriteAllText(filePath.ToString(), contents);
    //bug notify observer await patternNotesObserver.PatternLoaded(
    //  new SequenceDto(Transpose(sequenceDto.Steps, transpose)), cancellationToken);
  }

  private AbsoluteFilePath FilePathFor(PatternNumber patternNumber)
  {
    return Tb03PatternFileName.For(_folderPath, patternNumber.PatternGroupNumber, patternNumber.PatternNumberInGroup);
  }

  private ImmutableArray<SequenceStepDto> Transpose(ImmutableArray<SequenceStepDto> sequenceDtoSteps, int transpose)
  {
    //bug duplication. Transposition is also handled in another piece of code
    return sequenceDtoSteps.Select(s => s with { Note = s.Note + 12 * transpose}).ToImmutableArray();
  }
}