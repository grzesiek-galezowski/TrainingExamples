using System;
using System.Collections.Generic;

namespace SimpleNlp
{
  public static class StringExtensions
  {
    public static IEnumerable<string> SplitAndKeep(this string s, string separator)
    {
      var obj = s.Split(new[] { separator }, StringSplitOptions.None);
      var tokens = new List<string>();
      for (int i = 0; i < obj.Length; i++)
      {
        tokens.Add(obj[i]);
        if (i != obj.Length - 1)
        {
          tokens.Add(separator);
        }

      }
      return tokens;
    }
  }
}