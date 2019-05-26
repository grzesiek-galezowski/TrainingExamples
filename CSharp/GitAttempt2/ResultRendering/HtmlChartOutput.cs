using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Web;
using ApplicationLogic;

namespace ResultRendering
{
  public class HtmlChartOutput
  {

    public void InstantiateTemplate(AnalysisResult analysisResults)
    {
      var rankings = new StringBuilder();
      AddRanking(analysisResults.EntriesByDiminishingComplexity(), cl => cl.ComplexityOfCurrentVersion(), "Most complex", rankings);
      AddRanking(analysisResults.EntriesByDiminishingChangesCount(), cl => cl.ChangesCount(), "Most often changed", rankings);
      AddRanking(analysisResults.EntriesByDiminishingActivityPeriod(), cl => cl.ActivityPeriod(), "Longest active", rankings);
      AddRanking(analysisResults.EntriesFromMostRecentlyChanged(), cl => cl.LastChangeDate().ToString("d"), "Most recently changed (possible breeding grounds)", rankings);
      AddRanking(analysisResults.EntriesFromMostAncientlyChanged(), cl => cl.LastChangeDate().ToString("d"), "Most anciently changed (extract a library?)", rankings);

      var charts = new StringBuilder();
      AddCharts(analysisResults, charts);

      File.WriteAllText("output.html", 
        File.ReadAllText("mainTemplate.html")
          .Replace("___REPO___", analysisResults.Path)
          .Replace("___RANKINGS___", rankings.ToString())
          .Replace("___CHARTS___", charts.ToString())
        );
    }

    private void AddRanking<T>(
      IEnumerable<ChangeLog> entries, 
      Func<ChangeLog, T> valueFun, 
      string heading,
      StringBuilder result)
    {
      result.AppendLine($"<h2>{heading}:</h1>");
      result.AppendLine("<ol>");
      foreach (var changeLog in entries.Take(20))
      {
        result.AppendLine($"<li>{changeLog.PathOfCurrentVersion()} ({valueFun(changeLog)})</li>");
      }

      result.AppendLine("</ol>");
    }

    private static void AddCharts(AnalysisResult analysisResults, StringBuilder charts)
    {
      var elementNum = 0;
      foreach (var analysisResult in analysisResults.EntriesByHotSpotRank())
      {
        elementNum++;
        var singleFileChart = HtmlChartSingleResultTemplate.InstantiateWith(elementNum, analysisResult);
        charts.Append(singleFileChart);
      }
    }

    public void Show()
    {
      Browser.Open("output.html");
    }

  }

}