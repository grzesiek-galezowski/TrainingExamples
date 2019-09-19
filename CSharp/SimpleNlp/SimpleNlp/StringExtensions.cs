using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TddXt.SimpleNlp
{
  public static class StringExtensions
  {
    public static IEnumerable<string> SplitAndKeep(this string s, string separator)
    {
      var forbiddenDelimiter = "[^0-9a-zA-Z'`]";
      return Regex.Split(s, $"(?:{forbiddenDelimiter}|^)({separator})(?:{forbiddenDelimiter}|$)");
    }
  }
}