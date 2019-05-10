using System.Collections.Generic;
using System.IO;
using System.Linq;
using Functional.Maybe;

namespace GitAttempt2
{
  public static class ComplexityMetrics
  {
    public static double CalculateComplexityFor(string repositoryPath, string path)
    {
      var linesInFile = File.ReadLines(Path.Combine(repositoryPath, path)).ToArray();
      var complexity = CalculateComplexityFor(linesInFile);
      return complexity;
    }

    public static double CalculateComplexityFor(IReadOnlyCollection<string> linesInFile)
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