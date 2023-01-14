namespace Application.ApplicationLogic;

public interface ITb03PatternsFolder
{
  void LoadPattern(int patternGroupNumber, int patternNumberInGroup);
  void LoadPattern(PatternNumber patternNumber, IPatternNotesObserver patternNotesObserver);
  void LoadPattern(PatternNumber patternNumber, int transpose, IPatternNotesObserver patternNotesObserver);
}