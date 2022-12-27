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
    var endStepLine = HeaderLineWithSingleValue("END_STEP", Numerics.IntegerInt32);
    var tripletLine = HeaderLineWithSingleValue("TRIPLET", Numerics.IntegerInt32);

    var linesParser = (from stepNumber in EntryHeaderValueParser("STEP")
        from stateValue in EntryValueParser("STATE")
        from whiteSpace2 in Span.WhiteSpace
        from noteValue in EntryValueParser("NOTE")
        from whiteSpace3 in Span.WhiteSpace
        from accentValue in EntryValueParser("ACCENT")
        from whiteSpace4 in Span.WhiteSpace
        from slideValue in EntryValueParser("SLIDE")
        from endLine in Span.EqualTo(PrmNewLine)
        select new[] { stepNumber, stateValue, noteValue, accentValue, slideValue }
      ).Many();

    var textParser =
      from endStep in endStepLine
      from triplet in tripletLine
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
    var barsLine = HeaderLineWithSingleValue("BARS", Numerics.IntegerInt32);
    var dsBarLine = HeaderLineWithSingleValue("DS_BAR", Numerics.IntegerInt32);

    var linesParser = (from barNumber in EntryHeaderValueParser("BAR")
      from patternValue in EntryValueParser("PATTERN")
      from whiteSpace in Span.WhiteSpace
      from transposeValue in EntryValueParser("TRANSPOSE")
      from endLine in Span.EqualTo(PrmNewLine)
      select new[] { barNumber, patternValue, transposeValue }).Many();

    var textParser =
      from barsText in barsLine
      from dsBarText in dsBarLine
      from values in linesParser
      select TrackDtoFrom(barsText, dsBarText, values);

    var trackDto = textParser.Parse<TrackDto>(prmString);
    return trackDto;
  }

  private static TextParser<int> EntryValueParser(string key)
  {
    return from paramName in Span.EqualTo(key + "=")
      from paramValue in Numerics.IntegerInt32
      select paramValue;
  }

  private static TextParser<int> EntryHeaderValueParser(string header)
  {
    return from barHeader in Span.EqualTo(header)
        .Then(_ => Span.WhiteSpace)
      from barNumber in
        Numerics.IntegerInt32
      from assignment in Span.WhiteSpace
        .Then(_ => Character.EqualTo('='))
        .Then(_ => Span.WhiteSpace)
      select barNumber;
  }

  private static TextParser<int> HeaderLineWithSingleValue(string key, TextParser<int> valueParser)
  {
    return from assignment in Span.EqualTo(key)
        .Then(_ => Span.WhiteSpace)
        .Then(_ => Character.EqualTo('='))
        .Then(_ => Span.WhiteSpace)
      from value in valueParser
      from endLine in Span.EqualTo(PrmNewLine)
      select value;
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