using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using LibGit2Sharp;

namespace GitAttempt2
{

  //bug use own ids instead of oids or paths. Both are not reliable
  class Program
  {
    static void Main(string[] args)
    {
      //using var repo = new Repository(@"c:\Users\ftw637\Documents\GitHub\TrainingExamples\");
      using var repo = new Repository(@"C:\Users\grzes\Documents\GitHub\nscan\");
      var analysisMetadata = ScanAllCommits(repo);

      var pathsInTrunk = new List<string>();
      CollectPathsFrom(repo.Branches["master"].Commits.Reverse().Last().Tree, pathsInTrunk);

      var trunkFiles = analysisMetadata.Where(am => pathsInTrunk.Contains(am.Key))
        .Select(x => new TrunkFile(x.Value, x.Key));

      foreach (var trunkFile in trunkFiles.OrderBy(f => f.Metadata.ModifiedCommitsCount))
      {
        Console.WriteLine(trunkFile.Path + " => " + trunkFile.Metadata.ModifiedCommitsCount);
      }
    }

    private static Dictionary<string, AnalysisMetadata> ScanAllCommits(Repository repo)
    {
      var commitsPerPath = new Dictionary<string, AnalysisMetadata>();
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

    private static void AddCommit(Tree treeEntries, Dictionary<string, AnalysisMetadata> commitsPerPath)
    {
      foreach (var treeEntry in treeEntries)
      {
        switch (treeEntry.TargetType)
        {
          case TreeEntryTargetType.Blob:
            if (!commitsPerPath.ContainsKey(treeEntry.Path))
            {
              commitsPerPath[treeEntry.Path] = new AnalysisMetadata();
            }

            commitsPerPath[treeEntry.Path].IncreaseModifiedCommitsCount();

            break;
          case TreeEntryTargetType.Tree:
            AddCommit((Tree) treeEntry.Target, commitsPerPath);
            break;
          case TreeEntryTargetType.GitLink:
            throw new ArgumentException(treeEntry.Path);
          default:
            throw new ArgumentOutOfRangeException();
        }
      }

    }

    private static void AddFiles(TreeChanges treeChanges, Dictionary<string, AnalysisMetadata> commitsPerPath)
    {
      foreach (var treeEntry in treeChanges)
      {
        switch (treeEntry.Status)
        {
          case ChangeKind.Unmodified:
            break;
          case ChangeKind.Added:
            commitsPerPath[treeEntry.Path] = new AnalysisMetadata();
            commitsPerPath[treeEntry.Path].IncreaseModifiedCommitsCount();
            break;
          case ChangeKind.Deleted:
            break;
          case ChangeKind.Modified:
            commitsPerPath[treeEntry.Path].IncreaseModifiedCommitsCount();
            break;
          case ChangeKind.Renamed:
            commitsPerPath[treeEntry.Path] = commitsPerPath[treeEntry.OldPath];
            commitsPerPath[treeEntry.Path].IncreaseModifiedCommitsCount();
            break;
          case ChangeKind.Copied:
            commitsPerPath[treeEntry.Path] = new AnalysisMetadata();
            commitsPerPath[treeEntry.Path].IncreaseModifiedCommitsCount();
            break;
          default:
            throw new ArgumentOutOfRangeException();
        }
      }
    }

    private static void CollectPathsFrom(Tree tree, List<string> pathsByOid)
    {
      foreach (var treeEntry in tree)
      {
        switch (treeEntry.TargetType)
        {
          case TreeEntryTargetType.Blob:
            pathsByOid.Add(treeEntry.Path);
            break;
          case TreeEntryTargetType.Tree:
            CollectPathsFrom((Tree) treeEntry.Target, pathsByOid);
            break;
          case TreeEntryTargetType.GitLink:
            throw new ArgumentException(treeEntry.Path);
          default:
            throw new ArgumentOutOfRangeException();
        }
      }
    }
  }

  public class TrunkFile
  {
    public AnalysisMetadata Metadata { get; }
    public string Path { get; }

    public TrunkFile(AnalysisMetadata metadata, string path)
    {
      Metadata = metadata;
      Path = path;
    }
  }

  public class AnalysisMetadata
  {
    public int ModifiedCommitsCount { get; private set; } = 0;

    public void IncreaseModifiedCommitsCount()
    {
      ModifiedCommitsCount++;
    }
  }
}