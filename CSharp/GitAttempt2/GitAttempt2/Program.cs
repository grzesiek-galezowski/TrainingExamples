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
      var analysisResults = RepoAnalysis.Analyze(@"c:\Users\grzes\Documents\GitHub\nscan\", "master")
        .OrderByDescending(h => h.ChangesCount() * h.ComplexityOfLastVersion()).ToArray();

      foreach (var trunkFile in analysisResults)
      {
        Console.WriteLine(
          trunkFile.PathOfLastVersion() + " => " + 
          trunkFile.ChangesCount() + ":" + 
          trunkFile.ComplexityOfLastVersion());
      }

      var htmlChartRendering = new HtmlChartOutput();
      htmlChartRendering.InstantiateTemplate(analysisResults);
      htmlChartRendering.Render();
      htmlChartRendering.Show();
    }
  }
}