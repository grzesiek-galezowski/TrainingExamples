using System.Collections.Immutable;
using System.Linq;
using System.Text;
using Application.Ports;
using Superpower;
using Superpower.Parsers;

namespace Tb03Gui.Prm;

public static class PrmConvert
{
  private const string StateKey = "STATE";
  private const string PrmNewLine = "\n";
  private const string EndStep = "END_STEP";
  private const string Triplet = "TRIPLET";
  private const string Step = "STEP";
  private const string NoteKey = "NOTE";
  private const string AccentKey = "ACCENT";
  private const string SlideKey = "SLIDE";

  public static SequenceDto ParseIntoPattern(string prmString)
  {
    var endStepLine = HeaderLineWithSingleValue(EndStep, Numerics.IntegerInt32);
    var tripletLine = HeaderLineWithSingleValue(Triplet, Numerics.IntegerInt32);

    var linesParser = (from stepNumber in EntryHeaderValueParser(Step)
        from stateValue in EntryValueParser(StateKey)
        from whiteSpace2 in Span.WhiteSpace
        from noteValue in EntryValueParser(NoteKey)
        from whiteSpace3 in Span.WhiteSpace
        from accentValue in EntryValueParser(AccentKey)
        from whiteSpace4 in Span.WhiteSpace
        from slideValue in EntryValueParser(SlideKey)
        from endLine in Span.EqualTo(PrmNewLine)
        select new[] { stepNumber, stateValue, noteValue, accentValue, slideValue }
      ).Many();

    var textParser =
      from endStep in endStepLine
      from triplet in tripletLine
      from values in linesParser
      select PatternDtoFrom(values, endStep, triplet);

    var pattern = textParser.Parse(prmString);
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

  public static string ParseIntoString(ImmutableArray<SequenceStepDto> steps, int endStepIndex, int triplets)
  {
    var str = new StringBuilder();

    AppendGlobal(str, EndStep, endStepIndex);
    AppendGlobal(str, Triplet, triplets);
    foreach (var step in steps)
    {
      AppendOption(str, $"STEP {step.StepNumber}", _State(step.State), _Note(step.Note), _Accent(step.Accent), _Slide(step.Slide));
    }
    return str.ToString();
  }

  private static string _State(int value)
  {
    return Suboption(StateKey, value);
  }
  private static string _Note(int value)
  {
    return Suboption(NoteKey, value);
  }
  private static string _Accent(int value)
  {
    return Suboption(AccentKey, value);
  }
  private static string _Slide(int value)
  {
    return Suboption(SlideKey, value);
  }

  private static string Suboption(string key, int value)
  {
    return $"{key}={value}";
  }

  private static void AppendGlobal(StringBuilder content, string name, int value)
  {
    content.Append(name).Append("\t").Append("=").Append(" ").Append(value).Append(PrmNewLine);
  }

  private static void AppendOption(StringBuilder content, string name, params string[] suboptions)
  {
    content.Append(name).Append("\t").Append("=").Append(" ").Append(string.Join(" ", suboptions)).Append(PrmNewLine);
  }
}

/*
END_STEP	= 15
TRIPLET	= 0
STEP 1	= STATE=1 NOTE=31 ACCENT=0 SLIDE=0
STEP 2	= STATE=1 NOTE=31 ACCENT=0 SLIDE=0
STEP 3	= STATE=1 NOTE=31 ACCENT=0 SLIDE=0
STEP 4	= STATE=1 NOTE=31 ACCENT=0 SLIDE=0
STEP 5	= STATE=1 NOTE=31 ACCENT=0 SLIDE=0
STEP 6	= STATE=1 NOTE=31 ACCENT=0 SLIDE=0
STEP 7	= STATE=1 NOTE=31 ACCENT=0 SLIDE=0
STEP 8	= STATE=1 NOTE=31 ACCENT=0 SLIDE=0
STEP 9	= STATE=1 NOTE=34 ACCENT=0 SLIDE=0
STEP 10	= STATE=1 NOTE=34 ACCENT=0 SLIDE=0
STEP 11	= STATE=1 NOTE=34 ACCENT=0 SLIDE=0
STEP 12	= STATE=1 NOTE=34 ACCENT=0 SLIDE=0
STEP 13	= STATE=1 NOTE=36 ACCENT=0 SLIDE=0
STEP 14	= STATE=1 NOTE=36 ACCENT=0 SLIDE=0
STEP 15	= STATE=1 NOTE=36 ACCENT=0 SLIDE=0
STEP 16	= STATE=1 NOTE=36 ACCENT=0 SLIDE=0
*/