using System;
using System.Globalization;
using System.IO;
using System.Linq;
using ApplicationLogic;

namespace ResultRendering
{
  public static class HtmlChartSingleResultTemplate
  {
    public static string InstantiateWith(int elementNum, ChangeLog analysisResult)
    {
      var template = File.ReadAllText("chartTemplate.html");
      var replace = template
        .Replace("___COMPLEXITY___", analysisResult.ComplexityOfCurrentVersion().ToString(CultureInfo.InvariantCulture))
        .Replace("___CHANGES___", analysisResult.ChangesCount().ToString())
        .Replace("___CREATION_DATE___", analysisResult.CreationDate().ToString("d"))
        .Replace("___LAST_CHANGED___", analysisResult.LastChangeDate().ToString("d"))
        .Replace("___TIME_SINCE_LAST_CHANGED___", (int)analysisResult.TimeSinceLastChange().TotalDays + " days")
        .Replace("___ACTIVE_PERIOD___", (int)analysisResult.ActivityPeriod().TotalDays + " days")
        .Replace("___AGE___", (int)analysisResult.Age().TotalDays + " days")
        .Replace("___CHARTNUM___", elementNum.ToString())
        .Replace("___TITLE___", elementNum + ". " + analysisResult.PathOfCurrentVersion())
        .Replace("___Y_TITLE___", "Complexity per change")
        .Replace("___LABELS___", Labels(analysisResult))
        .Replace("___DATA___", Data(analysisResult));
      return replace;
    }

    private static string Data(ChangeLog changeLog)
    {
      var data = changeLog.Entries.Select(change => "'" + change.Complexity.ToString(CultureInfo.InvariantCulture) + "'");
      return String.Join(", ", data);
    }

    private static string Labels(ChangeLog changeLog)
    {
      var data = changeLog.Entries.Select(change => 
        "'" + change.ChangeDate.ToString("dd mm yyyy", CultureInfo.InvariantCulture) + "'");
      return String.Join(", ", data);
    }
  }
}