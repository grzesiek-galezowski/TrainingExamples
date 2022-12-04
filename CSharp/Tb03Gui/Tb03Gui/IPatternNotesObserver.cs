using System.Collections.Generic;
using Midi.Enums;
using MidiPlayground;

namespace Tb03Gui;

public interface IPatternNotesObserver
{
  void PatternLoaded(SequenceStepDto[] steps);
}