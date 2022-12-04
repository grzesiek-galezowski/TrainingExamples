using Midi.Enums;
using Superpower;
using Superpower.Parsers;

namespace MidiPlayground;

public static class PrmParser
{
  private const string PrmNewLine = "\n";

  public static SequenceStepDto[] ParseIntoPattern(string prmString)
  {
    var line1 = Span.EqualTo("END_STEP")
      .Then(_ => Span.WhiteSpace)
      .Then(_ => Character.EqualTo('='))
      .Then(_ => Span.WhiteSpace)
      .Then(_ => Numerics.IntegerInt32)
      .Then(_ => Span.EqualTo(PrmNewLine));

    var line2 = Span.EqualTo("TRIPLET")
      .Then(_ => Span.WhiteSpace)
      .Then(_ => Character.EqualTo('='))
      .Then(_ => Span.WhiteSpace)
      .Then(_ => Numerics.IntegerInt32)
      .Then(_ => Span.EqualTo(PrmNewLine));

    var linesParser = (from stepHeader in Span.EqualTo("STEP")
          .Then(_ => Span.WhiteSpace)
        from stepNumber in
          Numerics.IntegerInt32
        from stepAssignment in Span.WhiteSpace
          .Then(_ => Character.EqualTo('='))
          .Then(_ => Span.WhiteSpace)
        from statePreamble in Span.EqualTo("STATE=")
        from stateValue in Numerics.IntegerInt32
        from notePreamble in Span.WhiteSpace.Then(_ => Span.EqualTo("NOTE="))
        from noteValue in Numerics.IntegerInt32
        from accentPreamble in Span.WhiteSpace.Then(_ => Span.EqualTo("ACCENT="))
        from accentValue in Numerics.IntegerInt32
        from slidePreamble in Span.WhiteSpace.Then(_ => Span.EqualTo("SLIDE="))
        from slideValue in Numerics.IntegerInt32
        from endLine in Span.EqualTo(PrmNewLine)
        select new[] { stepNumber, stateValue, noteValue, accentValue, slideValue }
      ).Many();

    var textParser =
      from endStepText in line1
      from tripletText in line2
      from values in linesParser
      select PatternDtoFrom(values);

    var pattern = textParser.Parse<SequenceStepDto[]>(prmString);
    return pattern;
  }

  private static SequenceStepDto[] PatternDtoFrom(int[][] values)
  {
    return values.Select(v => new SequenceStepDto
    {
      StepNumber = v[0],
      State = v[1], 
      Note = v[2], 
      Accent = v[3], 
      Slide = v[4]
    }).ToArray();
  }

  public static List<Pitch> TranslateIntoMidiPitches(SequenceStepDto[] pattern)
  {
    var melody = new List<Pitch>();
    foreach (var stepDto in pattern)
    {
      melody.Add((Pitch)stepDto.Note);
    }

    return melody;
  }
}