using System;

namespace Tb03Gui.ApplicationLogic;

public record PatternNumber
{
  private const int TotalPatternsInGroup = 24;
  public int PatternGroupNumber { get; }
  public int PatternNumberInGroup { get; }
  public int FlatPatternNumber => ((PatternGroupNumber - 1) * TotalPatternsInGroup) + PatternNumberInGroup;

  private PatternNumber(int patternGroupNumber, int patternNumberInGroup)
  {
    PatternGroupNumber = patternGroupNumber;
    PatternNumberInGroup = patternNumberInGroup;
  }

  public static PatternNumber FromFlatNumber(int patternNumber)
  {
    //bug change the constructor when I add from group and number
    var patternNumberInGroup = patternNumber % TotalPatternsInGroup;
    if (patternNumberInGroup == 0)
    {
      patternNumberInGroup = TotalPatternsInGroup;
    }
    return new PatternNumber(
      (int)Math.Ceiling((float)patternNumber / TotalPatternsInGroup), 
      patternNumberInGroup);
  }

  public static PatternNumber FromGroupAndNumberInGroup(int patternGroupNumber, int patternNumberInGroup)
  {
    return new PatternNumber(patternGroupNumber, patternNumberInGroup);
  }
}