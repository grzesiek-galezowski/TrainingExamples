using Application.Ports;
using Core.Maybe;

namespace Application;

internal class NoActivePatternsFolder : ITb03PatternsFolder
{
  public async Task LoadPattern(int patternGroupNumber, int patternNumberInGroup, CancellationToken cancellationToken)
  {
  }

  public async Task LoadPattern(PatternNumber patternNumber, int transpose, IPatternNotesObserver patternNotesObserver,
    CancellationToken cancellationToken)
  {
  }

  public void SavePattern(Maybe<Tb03Note>[] notes, int sequenceLength, PatternNumber patternNumber)
  {
    
  }
}