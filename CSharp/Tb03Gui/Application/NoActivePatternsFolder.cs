using Application.Ports;

namespace Application;

internal class NoActivePatternsFolder : ITb03PatternsFolder
{
  public async Task LoadPattern(int patternGroupNumber, int patternNumberInGroup, CancellationToken cancellationToken)
  {
  }

  public void LoadPattern(PatternNumber patternNumber, IPatternNotesObserver patternNotesObserver)
  {
  }

  public async Task LoadPattern(PatternNumber patternNumber, int transpose, IPatternNotesObserver patternNotesObserver,
    CancellationToken cancellationToken)
  {
  }
}