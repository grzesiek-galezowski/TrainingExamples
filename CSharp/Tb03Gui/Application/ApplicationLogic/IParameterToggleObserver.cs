namespace Application.ApplicationLogic;

public interface IParameterToggleObserver
{
  void OnAccentChanged(bool newAccentValue);
  void OnSlideChanged(bool newSlideValue);
}