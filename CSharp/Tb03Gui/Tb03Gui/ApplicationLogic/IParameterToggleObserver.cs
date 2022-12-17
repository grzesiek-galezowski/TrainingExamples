namespace Tb03Gui.ApplicationLogic;

public interface IParameterToggleObserver
{
  void OnAccentChanged(bool newAccentValue);
  void OnSlideChanged(bool newSlideValue);
}