using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Chart.Mvc.ComplexChart;
using Chart.Mvc.Extensions;

namespace GitAttempt2
{
  public class Program
  {
    static void Main(string[] args)
    {
      //git log --format=format: --name-only | egrep -v '^$' | sort | uniq -c | sort -r | head -5

      var trunkFiles = RepoAnalysis.Analyze(@"c:\Users\grzes\Documents\GitHub\nscan\", "master")
        .OrderByDescending(f => f.ChangeRate * f.History.Last().Complexity);
      //var trunkFiles = RepoAnalysis.Analyze(@"C:\Users\grzes\Documents\GitHub\functional-maybe-extensions ", "master");

      foreach (var trunkFile in trunkFiles)
      {
        Console.WriteLine(trunkFile.Path + " => " + trunkFile.ChangeRate + ":" + trunkFile.History.Last().Complexity);
      }

      RenderChart(trunkFiles);

      //trunkFiles.First()
    }

    private static void RenderChart(IEnumerable<HistoryAnalysisResult> analysisResults)
    {
      string charts = "";
      int i = 0;
      foreach (var analysisResult in analysisResults)
      {
        var template = File.ReadAllText("chartTemplate.html");
        charts += template
          .Replace("___COMPLEXITY___", analysisResult.History.Last().Complexity.ToString())
          .Replace("___CHANGES___", analysisResult.History.Count.ToString())
          .Replace("___CHARTNUM___", i++.ToString())
          .Replace("___TITLE___", analysisResult.Path)
          .Replace("___LABELS___", Labels(analysisResult))
          .Replace("___DATA___", Data(analysisResult));
      }
      File.WriteAllText("output.html", File.ReadAllText("mainTemplate.html").Replace("___CHARTS___", charts));

      OpenBrowser("output.html");
    }

    private static string Data(HistoryAnalysisResult historyAnalysisResult)
    {
      var data = historyAnalysisResult.History.Select(change => "'" + change.Complexity.ToString(CultureInfo.InvariantCulture) + "'");
      return string.Join(", ", data);
    }

    private static string Labels(HistoryAnalysisResult historyAnalysisResult)
    {
      var data = historyAnalysisResult.History.Select(change => 
        "'" + change.ChangeDate.ToString("dd mm yyyy", CultureInfo.InvariantCulture) + "'");
      return string.Join(", ", data);
    }

    public static void OpenBrowser(string url)
    {
      try
      {
        Process.Start(url);
      }
      catch
      {
        // hack because of this: https://github.com/dotnet/corefx/issues/10361
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
          url = url.Replace("&", "^&");
          Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
          Process.Start("xdg-open", url);
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
          Process.Start("open", url);
        }
        else
        {
          throw;
        }
      }
    }
  }
}