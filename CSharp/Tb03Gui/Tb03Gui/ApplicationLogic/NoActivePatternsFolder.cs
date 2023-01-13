namespace Tb03Gui.ApplicationLogic;

internal class NoActivePatternsFolder : ITb03PatternsFolder
{
  public void LoadPattern(int patternGroupNumber, int patternNumberInGroup)
  {
  }

  public void LoadPattern(PatternNumber patternNumber, IPatternNotesObserver patternNotesObserver)
  {
  }

  public void LoadPattern(PatternNumber patternNumber, int transpose, IPatternNotesObserver patternNotesObserver)
  {
  }
}