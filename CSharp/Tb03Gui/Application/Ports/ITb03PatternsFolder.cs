using Core.Maybe;

namespace Application.Ports;

public interface ITb03PatternsFolder
{
  Task LoadPattern(int patternGroupNumber, int patternNumberInGroup, CancellationToken cancellationToken);
  Task LoadPattern(PatternNumber patternNumber, int transpose, IPatternLoadingObserver patternLoadingObserver,
    CancellationToken cancellationToken);

  void SavePattern(Maybe<Tb03Note>[] notes, int sequenceLength, PatternNumber patternNumber,
    IPatternSavingObserver patternLoadingObserver);
}