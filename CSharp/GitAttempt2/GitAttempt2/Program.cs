using System;
using System.Linq;

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
    }
  }
}