﻿using System;

namespace Tb03Gui.ApplicationLogic;

public record PatternNumber
{
  private const int TotalPatternsInGroup = 24;
  public int PatternGroupNumber { get; }
  public int PatternNumberInGroup { get; }
  public int FlatPatternNumber { get; }

  private PatternNumber(int patternGroupNumber, int patternNumberInGroup, int flatPatternNumber)
  {
    PatternGroupNumber = patternGroupNumber;
    PatternNumberInGroup = patternNumberInGroup;
    FlatPatternNumber = flatPatternNumber;
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
      patternNumberInGroup, 
      patternNumber);
  }

  public static PatternNumber FromGroupAndNumberInGroup(int patternGroupNumber, int patternNumberInGroup)
  {
    return new PatternNumber(
      patternGroupNumber, 
      patternNumberInGroup, 
      (patternGroupNumber - 1) * TotalPatternsInGroup + patternNumberInGroup);
  }
}