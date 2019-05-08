using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using LibGit2Sharp;

namespace GitAttempt2
{
  //bug use own ids instead of oids or paths. Both are not reliable
  class Program
  {
    static void Main(string[] args)
    {
      //using var repo = new Repository(@"c:\Users\ftw637\Documents\GitHub\TrainingExamples\")
      using var repo = new Repository(@"C:\Users\grzes\Documents\GitHub\TrainingExamples\");
      var analysisMetadata = ScanAllCommits(repo);

      var pathsByOid = new Dictionary<ObjectId, string>();
      PrintTreeOf(repo.Branches["master"].Commits.Reverse().Last().Tree, pathsByOid);
      
      foreach (var entry in pathsByOid)
      {
        Console.WriteLine(entry.Value + " => " + analysisMetadata[entry.Key].ModifiedCommitsCount);
      }
    }

    private static Dictionary<ObjectId, AnalysisMetadata> ScanAllCommits(Repository repo)
    {
      var commitsPerPath = new Dictionary<ObjectId, AnalysisMetadata>();
      var commits = repo.Branches["master"].Commits.Reverse().ToArray();

      Commit first = commits.First();
      AddCommit(first.Tree, commitsPerPath);
      for (var i = 1; i < commits.Length; ++i)
      {
        var previousCommit = commits[i - 1];
        var currentCommit = commits[i];

        AddFiles(repo.Diff.Compare<TreeChanges>(previousCommit.Tree, currentCommit.Tree), commitsPerPath);
      }

      return commitsPerPath;
    }

    private static void AddCommit(Tree treeEntries, Dictionary<ObjectId, AnalysisMetadata> commitsPerPath)
    {
      foreach (var treeEntry in treeEntries)
      {
        switch (treeEntry.TargetType)
        {
          case TreeEntryTargetType.Blob:
            if (!commitsPerPath.ContainsKey(treeEntry.Target.Id))
            {
              commitsPerPath[treeEntry.Target.Id] = new AnalysisMetadata();
            }

            commitsPerPath[treeEntry.Target.Id].AddAlias(treeEntry.Path);
            commitsPerPath[treeEntry.Target.Id].IncreaseModifiedCommittsCount();

            break;
          case TreeEntryTargetType.Tree:
            AddCommit((Tree)treeEntry.Target, commitsPerPath);
            break;
          case TreeEntryTargetType.GitLink:
            throw new ArgumentException(treeEntry.Path);
          default:
            throw new ArgumentOutOfRangeException();
        }
      }

    }

    private static void AddFiles(TreeChanges treeChanges, Dictionary<ObjectId, AnalysisMetadata> commitsPerPath)
    {
      foreach (var treeEntry in treeChanges)
      {
        if (!commitsPerPath.ContainsKey(treeEntry.Oid))
        {
          if (treeEntry.Path.Contains(".gitattributes"))
          {
            Console.WriteLine("YESSS");
          }
          commitsPerPath[treeEntry.Oid] = new AnalysisMetadata();
        }

        commitsPerPath[treeEntry.Oid].AddAlias(treeEntry.Path);
        commitsPerPath[treeEntry.Oid].IncreaseModifiedCommittsCount();
      }
    }

    private static void PrintTreeOf(Tree tree, Dictionary<ObjectId, string> pathsByOid)
    {
      foreach (var treeEntry in tree)
      {
        switch (treeEntry.TargetType)
        {
          case TreeEntryTargetType.Blob:
            pathsByOid[treeEntry.Target.Id] = treeEntry.Path;
            break;
          case TreeEntryTargetType.Tree:
            PrintTreeOf((Tree) treeEntry.Target, pathsByOid);
            break;
          case TreeEntryTargetType.GitLink:
            throw new ArgumentException(treeEntry.Path);
          default:
            throw new ArgumentOutOfRangeException();
        }
      }
    }
  }

  public class AnalysisMetadata
  {
    public List<string> _aliases = new List<string>();
    public int ModifiedCommitsCount { get; set; } = 0;

    public void AddAlias(string path)
    {
      _aliases.Add(path);
    }

    public void IncreaseModifiedCommittsCount()
    {
      ModifiedCommitsCount++;
    }
  }
}