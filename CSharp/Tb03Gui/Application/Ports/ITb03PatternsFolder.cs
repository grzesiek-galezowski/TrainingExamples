namespace Application.Ports;

public interface ITb03PatternsFolder
{
  Task LoadPattern(int patternGroupNumber, int patternNumberInGroup, CancellationToken cancellationToken);
  Task LoadPattern(PatternNumber patternNumber, int transpose, IPatternNotesObserver patternNotesObserver,
    CancellationToken cancellationToken);
}