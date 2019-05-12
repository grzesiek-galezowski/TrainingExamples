using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using ApplicationLogic;

namespace ResultRendering
{
  public class HtmlChartOutput
  {
    private string _charts = "";
    private int _elementNum = 0;

    public void InstantiateTemplate(IEnumerable<ChangeLog> analysisResults)
    {
      foreach (var analysisResult in analysisResults)
      {
        _elementNum++;
        var template = File.ReadAllText("chartTemplate.html");
        _charts += template
          .Replace("___COMPLEXITY___", analysisResult.ComplexityOfLastVersion().ToString(CultureInfo.InvariantCulture))
          .Replace("___CHANGES___", analysisResult.ChangesCount().ToString())
          .Replace("___CHARTNUM___", _elementNum.ToString())
          .Replace("___TITLE___", _elementNum + ". " + analysisResult.PathOfLastVersion())
          .Replace("___Y_TITLE___", "Complexity per change")
          .Replace("___LABELS___", Labels(analysisResult))
          .Replace("___DATA___", Data(analysisResult));
      }
    }

    public void Render()
    {
      File.WriteAllText("output.html", File.ReadAllText("mainTemplate.html").Replace("___CHARTS___", _charts));
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

    public void Show()
    {
      OpenBrowser("output.html");
    }
  }
}