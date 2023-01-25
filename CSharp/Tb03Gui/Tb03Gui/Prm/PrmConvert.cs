using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Application.Ports;
using Midi.Enums;
using Superpower;
using Superpower.Parsers;

namespace Tb03Gui.Prm;

public static class PrmConvert
{
  private const string PrmNewLine = "\n";

  public static SequenceDto ParseIntoPattern(string prmString)
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
      select PatternDtoFrom(values, endStep, triplet);

    var pattern = textParser.Parse<SequenceDto>(prmString);
    return pattern;
  }

  private static SequenceDto PatternDtoFrom(
    int[][] values, 
    int endStep, 
    int triplet)
  {
    return new SequenceDto(
      endStep,
      triplet,
      values.Select(v => new SequenceStepDto
      {
        StepNumber = v[0],
        State = v[1],
        Note = v[2],
        Accent = v[3],
        Slide = v[4]
      }).ToImmutableArray());
  }
  
  private static TrackDto TrackDtoFrom(int bars, int dalSegnoBar, int[][] values)
  {
    var entries = values.Select(v => new TrackEntryDto(v[0], v[1], v[2])).ToArray();
    return new TrackDto(bars, dalSegnoBar, entries);
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
      from barsValue in barsLine
      from dalSegnoBarValue in dsBarLine
      from values in linesParser
      select TrackDtoFrom(barsValue, dalSegnoBarValue, values);

    var trackDto = textParser.Parse(prmString);
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

  public static List<Pitch> TranslateIntoMidiPitches(ImmutableArray<SequenceStepDto> pattern)
  {
    var melody = new List<Pitch>();
    foreach (var stepDto in pattern)
    {
      melody.Add((Pitch)stepDto.Note);
    }

    return melody;
  }

  public static object ParseIntoString(ImmutableArray<SequenceStepDto> steps, int endStepIndex, int triplets)
  {
    throw new System.NotImplementedException();
  }
}