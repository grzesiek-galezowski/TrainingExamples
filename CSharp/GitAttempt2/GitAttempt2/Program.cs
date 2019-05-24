using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationLogic;
using ResultRendering;

namespace GitAttempt2
{
  public class Program
  {
    //TODO calculate age of code (1st commit to now)
    //TODO calculate age of code (1st commit to last commit)
    //TODO calculate time since last commit
    static void Main(string[] args)
    {
      Console.WriteLine(TimeSpan.FromDays(12));
      var changeLogs = RepoAnalysis.Analyze(@"c:\Users\grzes\Documents\GitHub\nscan\", "master")
        .OrderByDescending(h => h.HotSpotPosition()).ToArray();

      new ConsoleRendering().Show(changeLogs);

      var htmlChartRendering = new HtmlChartOutput();
      htmlChartRendering.InstantiateTemplate(changeLogs);
      htmlChartRendering.Show();
    }
  }
}