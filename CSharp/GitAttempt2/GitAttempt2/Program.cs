using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using ApplicationLogic;

namespace GitAttempt2
{
  public class Program
  {
    static void Main(string[] args)
    {
      var analysisResults = RepoAnalysis.Analyze(@"c:\Users\grzes\Documents\GitHub\nscan\", "master")
        .OrderByDescending(h => h.ChangesCount() * h.ComplexityOfLastVersion()).ToArray();

      foreach (var trunkFile in analysisResults)
      {
        Console.WriteLine(trunkFile.PathOfLastVersion() + " => " + trunkFile.ChangesCount() + ":" + trunkFile.ComplexityOfLastVersion());
      }

      RenderChart(analysisResults);

    }

    private static void RenderChart(ChangeLog[] analysisResults)
    {
      var charts = "";
      var i = 0;
      foreach (var analysisResult in analysisResults)
      {
        i++;
        var template = File.ReadAllText("chartTemplate.html");
        charts += template
          .Replace("___COMPLEXITY___", analysisResult.ComplexityOfLastVersion().ToString())
          .Replace("___CHANGES___", analysisResult.ChangesCount().ToString())
          .Replace("___CHARTNUM___", i.ToString())
          .Replace("___TITLE___", i + ". " + analysisResult.PathOfLastVersion())
          .Replace("___Y_TITLE___", "Complexity per change")
          .Replace("___LABELS___", Labels(analysisResult)
          .Replace("___DATA___", Data(analysisResult)));
      }
      File.WriteAllText("output.html", File.ReadAllText("mainTemplate.html").Replace("___CHARTS___", charts));

      OpenBrowser("output.html");
    }

    private static string Data(ChangeLog changeLog)
    {
      var data = changeLog.Entries.Select(change => "'" + change.Complexity.ToString(CultureInfo.InvariantCulture) + "'");
      return string.Join(", ", data);
    }

    private static string Labels(ChangeLog changeLog)
    {
      var data = changeLog.Entries.Select(change => 
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