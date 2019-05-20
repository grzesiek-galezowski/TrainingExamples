using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationLogic;
using ResultRendering;

namespace GitAttempt2
{
  public class Program
  {
    static void Main(string[] args)
    {
      var changeLogs = RepoAnalysis.Analyze(@"c:\Users\grzes\Documents\GitHub\nscan\", "master")
        .OrderByDescending(h => h.ChangesCount() * h.ComplexityOfLastVersion()).ToArray();

      new ConsoleRendering().Show(changeLogs);

      var htmlChartRendering = new HtmlChartOutput();
      htmlChartRendering.InstantiateTemplate(changeLogs);
      htmlChartRendering.Show();
    }
  }
}