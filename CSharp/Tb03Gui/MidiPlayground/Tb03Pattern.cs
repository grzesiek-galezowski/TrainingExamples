using Midi.Enums;

namespace MidiPlayground;

public class Tb03Pattern
{
  private readonly SequenceStepDto[] _pattern;

  public Tb03Pattern(SequenceStepDto[] parseIntoPattern)
  {
    _pattern = parseIntoPattern;
  }

  public Task PlayOn(Synth synth)
  {
    return synth.Play(ReadMelody());
  }

  private List<Pitch> ReadMelody()
  {
    return PrmParser.TranslateIntoMidiPitches(_pattern);
  }
}