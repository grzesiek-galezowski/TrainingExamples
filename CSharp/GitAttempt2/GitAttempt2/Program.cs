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
    //TODO histogram of age
    //TODO unfolding https://codepen.io/crucialfelix/pen/jiztn
    static void Main(string[] args)
    {
      Console.WriteLine(TimeSpan.FromDays(12));
      var analysisResult = RepoAnalysis.Analyze(@"C:\Users\grzes\Documents\GitHub\nscan\", "master");

      new ConsoleRendering().Show(analysisResult);

      var htmlChartRendering = new HtmlChartOutput();
      htmlChartRendering.InstantiateTemplate(analysisResult);
      htmlChartRendering.Show();
    }
  }
}