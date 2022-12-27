using Midi.Enums;
using Superpower;
using Superpower.Model;
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

  private static TrackDto TrackDtoFrom(int bars, int dsBar, int[][] values)
  {
    var entries = values.Select(v => new TrackEntryDto(v[0], v[1], v[2])).ToArray();
    return new TrackDto(bars, dsBar, entries);
  }

  public static TrackDto ParseIntoTrack(string prmString)
  {
    var barsLine = from barsAssignment in Span.EqualTo("BARS")
      .Then(_ => Span.WhiteSpace)
      .Then(_ => Character.EqualTo('='))
      .Then(_ => Span.WhiteSpace)
      from barsValue in Numerics.IntegerInt32
      from endLine in Span.EqualTo(PrmNewLine)
      select barsValue;

    var dsBarLine = from dsBarAssignment in Span.EqualTo("DS_BAR")
      .Then(_ => Span.WhiteSpace)
      .Then(_ => Character.EqualTo('='))
      .Then(_ => Span.WhiteSpace)
      from dsBarValue in Numerics.IntegerInt32
      from newLine in Span.EqualTo(PrmNewLine)
      select dsBarValue;

    var linesParser = (from barHeader in Span.EqualTo("BAR")
          .Then(_ => Span.WhiteSpace)
        from barNumber in
          Numerics.IntegerInt32
        from barAssignment in Span.WhiteSpace
          .Then(_ => Character.EqualTo('='))
          .Then(_ => Span.WhiteSpace)
        from patternPreamble in Span.EqualTo("PATTERN=")
        from patternValue in Numerics.IntegerInt32
        from transposePreamble in Span.WhiteSpace.Then(_ => Span.EqualTo("TRANSPOSE="))
        from transposeValue in Numerics.IntegerInt32
        from endLine in Span.EqualTo(PrmNewLine)
        select new[] { barNumber, patternValue, transposeValue }
      ).Many();

    var textParser =
      from barsText in barsLine
      from dsBarText in dsBarLine
      from values in linesParser
      select TrackDtoFrom(barsText, dsBarText, values);

    var pattern = textParser.Parse<TrackDto>(prmString);
    return pattern;
    
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