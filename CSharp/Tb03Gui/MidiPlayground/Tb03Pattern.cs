using Midi.Enums;

namespace MidiPlayground;

public class Tb03Pattern
{
  private readonly SequenceDto _pattern;

  public Tb03Pattern(SequenceDto sequenceDto)
  {
    _pattern = sequenceDto;
  }

  public void PlayOn(Synthesizer synthesizer)
  {
    synthesizer.Play(ReadMelody());
  }

  private List<Pitch> ReadMelody()
  {
    return PrmParser.TranslateIntoMidiPitches(_pattern.Steps);
  }
}