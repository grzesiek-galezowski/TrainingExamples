namespace Tb03Gui;

public interface IPatternNavigationObserver
{
  void OnPatternGroupSelectionChanged(int patternGroupNumber);
  void OnPatternSelectionChanged(int patternNumber);
}