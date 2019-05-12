using System;
using ApplicationLogic;

namespace GitAttempt2
{
  public static class ChangeFactory
  {
    public static Change CreateChange(string path, string fileText, DateTimeOffset changeDate)
    {
      var contentText = fileText;
      return new Change(
        path, 
        contentText, ComplexityMetrics.CalculateComplexityFor(contentText),
        changeDate);
    }
  }
}