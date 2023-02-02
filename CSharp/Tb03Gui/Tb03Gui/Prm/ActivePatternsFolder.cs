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
  private readonly IPatternLoadingObserver _patternLoadingObserver;

  public ActivePatternsFolder(
    AbsoluteDirectoryPath folderPath,
    IPatternLoadingObserver patternLoadingObserver)
  {
    _folderPath = folderPath;
    _patternLoadingObserver = patternLoadingObserver;
  }

  public async Task LoadPattern(int patternGroupNumber, int patternNumberInGroup, CancellationToken cancellationToken)
  {
    var patternNumber = PatternNumber.FromGroupAndNumberInGroup(patternGroupNumber, patternNumberInGroup);
    await LoadPattern(patternNumber, 0, _patternLoadingObserver, cancellationToken);
  }

  public async Task LoadPattern(
    PatternNumber patternNumber,
    int transpose,
    IPatternLoadingObserver patternLoadingObserver,
    CancellationToken cancellationToken)
  {
    var fileName = FilePathFor(patternNumber);
    var fileContent = await File.ReadAllTextAsync(fileName.ToString(), cancellationToken);
    var sequenceDto = PrmConvert.ParseIntoPattern(fileContent);
    await patternLoadingObserver.PatternLoaded(
      sequenceDto with
      {
        Steps = Transpose(sequenceDto.Steps, transpose)
      },
      cancellationToken);
  }

  public void SavePattern(
    Maybe<Tb03Note>[] notes,
    int sequenceLength,
    PatternNumber patternNumber,
    IPatternSavingObserver patternLoadingObserver)
  {
    //bug somehow pass end note and triplet
    //bug the "test" extension is to avoid overriding accidentally the original files.
    var filePath = FilePathFor(patternNumber).ChangeExtensionTo(FileExtension.Value(".test"));
    var contents = PrmConvert.ParseIntoString(
      ToSequenceStepDtos(notes), 
      sequenceLength-1,  
      0 /* bug for now constant triplets param value */);
    File.WriteAllText(filePath.ToString(), contents);
    patternLoadingObserver.PatternSaved(filePath);
  }

  private static ImmutableArray<SequenceStepDto> ToSequenceStepDtos(Maybe<Tb03Note>[] notes)
  {
    return notes.Select((n, i) => new SequenceStepDto
    {
      Accent = Convert.ToInt32(n.Value().Accent),
      Note = n.Value().Pitch,
      Slide = Convert.ToInt32(n.Value().Slide),
      State = n.Value().State,
      StepNumber = i + 1
    }).ToImmutableArray();
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