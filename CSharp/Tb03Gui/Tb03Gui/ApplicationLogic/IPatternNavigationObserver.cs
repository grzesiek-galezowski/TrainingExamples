namespace Tb03Gui.ApplicationLogic;

public interface IPatternNavigationObserver
{
  void OnPatternGroupSelectionChanged(int patternGroupNumber);
  void OnPatternSelectionChanged(int patternNumber);
}