using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using LibGit2Sharp;

namespace GitAttempt2
{
  class Program
  {
    static void Main(string[] args)
    {
      var commitsPerPath = new Dictionary<string, int>();
      using (var repo = new Repository(@"C:\Users\grzes\Documents\GitHub\nscan"))
      {
        var commits = repo.Commits.ToArray();
        for (int i = 1; i < commits.Count(); ++i)
        {
          AddToChanges(repo, commitsPerPath, commits[i], commits[i - 1]);
          PrintTreeOf(commits[i].Tree);

        }
      }
      /*
      foreach (var keyValuePair in commitsPerPath.OrderBy(kvp => kvp.Value))
      {
        Console.WriteLine($"{keyValuePair.Key} => {keyValuePair.Value}");
      }*/
    }

    private static void PrintTreeOf(Tree tree)
    {
      foreach (var treeEntry in tree)
      {
        switch (treeEntry.TargetType)
        {
          case TreeEntryTargetType.Blob:
            Console.WriteLine(treeEntry.Path);
            break;
          case TreeEntryTargetType.Tree:
            Console.WriteLine(treeEntry.Path);
            PrintTreeOf((Tree)treeEntry.Target);
            break;
          case TreeEntryTargetType.GitLink:
            throw new ArgumentException(treeEntry.Path);
          default:
            throw new ArgumentOutOfRangeException();
        }
      }
    }

    private static void AddToChanges(
      IRepository repo, 
      IDictionary<string, int> commitsPerPath, 
      Commit currentCommit, 
      Commit previousCommit)
    {
      var treeChanges = repo.Diff.Compare<TreeChanges>(currentCommit.Tree, previousCommit.Tree);
      foreach (var treeEntry in treeChanges)
      {
        if (commitsPerPath.ContainsKey(treeEntry.Path))
        {
          commitsPerPath[treeEntry.Path]++;
        }
        else
        {
          commitsPerPath[treeEntry.Path] = 1;
        }
      }
    }
  }
}
