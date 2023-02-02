namespace Application.Ports;

public interface IParameterToggleObserver
{
  void OnAccentChanged(bool newAccentValue);
  void OnSlideChanged(bool newSlideValue);
}