using System;
using System.Text.RegularExpressions;
using ApplicationLogic;
using LibGit2Sharp;

namespace GitAttempt2
{
  public static class ChangeFactory
  {
    public static Change CreateChange(string path, Blob blob, DateTimeOffset changeDate)
    {
      var contentText = blob.GetContentText();
      return new Change(
        path, 
        contentText, ComplexityMetrics.CalculateComplexityFor(contentText),
        changeDate);
    }
  }
}