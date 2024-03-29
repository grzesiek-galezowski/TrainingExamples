﻿namespace Application.Ports;

public interface IPatternNavigationObserver
{
  void OnPatternGroupSelectionChanged(int patternGroupNumber);
  void OnPatternSelectionChanged(int patternNumber);
}