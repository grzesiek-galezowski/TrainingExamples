﻿namespace Application.Ports;

public interface ITb03TracksFolder
{
  void LoadTrack(int trackNumber, ITrackPatternsObserver observer);
}