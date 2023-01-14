using System.Linq;
using Application.ApplicationLogic;

namespace Tb03Gui.Midi;

public class Tb03Pattern
{
  private readonly SequenceDto _pattern;

  public Tb03Pattern(SequenceDto sequenceDto)
  {
    _pattern = sequenceDto;
  }

  public void PlayOn(Synthesizer synthesizer)
  {
    synthesizer.Play(_pattern.Steps.Select(s => s.Note).ToList());
  }
}