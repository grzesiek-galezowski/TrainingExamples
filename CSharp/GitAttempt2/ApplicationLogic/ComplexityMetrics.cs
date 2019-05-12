using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Functional.Maybe;

namespace ApplicationLogic
{
  public static class ComplexityMetrics
  {
    public static double CalculateComplexityFor(string contentText)
    {
      return CalculateComplexityFor(Regex.Split(contentText, @"\r\n|\r|\n"));
    }

    private static double CalculateComplexityFor(IReadOnlyCollection<string> linesInFile)
    {
      var totalWhitespaces = 0;
      var currentIndentationLength = Maybe<int>.Nothing;
      foreach (var line in linesInFile)
      {
        var lineIndentation = IndentationOf(line);
        if (ThereIsAny(lineIndentation) && IsBetter(lineIndentation, currentIndentationLength))
        {
          currentIndentationLength = lineIndentation.ToMaybe();
        }

        totalWhitespaces += lineIndentation;
      }

      var complexity = (TotalIndentations(totalWhitespaces, currentIndentationLength) + linesInFile.Count) /
                       linesInFile.Count;
      return complexity;
    }

    private static bool IsBetter(int lineIndentation, Maybe<int> currentIndentationLength)
    {
      return (!currentIndentationLength.HasValue || lineIndentation < currentIndentationLength.Value);
    }

    private static bool ThereIsAny(int lineIndentation)
    {
      return lineIndentation > 0;
    }

    private static double TotalIndentations(int totalWhitespaces, Maybe<int> indentationLength)
    {
      return 1d * totalWhitespaces / indentationLength.Select(i => i + 1).OrElse(1);
    }

    private static int IndentationOf(string line)
    {
      return line.TakeWhile(char.IsWhiteSpace).Count();
    }

  }
}