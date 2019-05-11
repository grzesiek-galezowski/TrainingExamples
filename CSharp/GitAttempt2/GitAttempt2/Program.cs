using System;
using System.Collections.Generic;
using System.Diagnostics;
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

      var trunkFiles = RepoAnalysis.Analyze(@"c:\Users\grzes\Documents\GitHub\nscan\", "master");
      //var trunkFiles = RepoAnalysis.Analyze(@"C:\Users\grzes\Documents\GitHub\functional-maybe-extensions ", "master");

      foreach (var trunkFile in trunkFiles.OrderBy(f => f.ChangeRate * f.Complexity))
      {
        Console.WriteLine(trunkFile.Path + " => " + trunkFile.ChangeRate + ":" + trunkFile.Complexity);
      }

      RenderChart(trunkFiles);

      //trunkFiles.First()
    }

    private static void RenderChart(IEnumerable<TrunkFile> trunkFiles)
    {
      var template = File.ReadAllText("template.html");
      File.WriteAllText("output.html", template.Replace("___LABELS___", "'lol1', 'lol2'").Replace("___DATA___", "'2', '3'"));
      OpenBrowser("output.html");
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