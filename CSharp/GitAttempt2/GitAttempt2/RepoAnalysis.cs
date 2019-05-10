using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Functional.Maybe;
using LibGit2Sharp;

namespace GitAttempt2
{
  public static class RepoAnalysis
  {
    public static IEnumerable<TrunkFile> Analyze(string repositoryPath, string branchName)
    {
      //using var repo = new Repository(@"c:\Users\ftw637\Documents\GitHub\TrainingExamples\");
      //using var repo = new Repository(@"C:\Users\grzes\Documents\GitHub\nscan\");

      using var repo = new Repository(repositoryPath);

      IEnumerable<LogEntry> logEntries = repo.Commits.QueryBy("src/NScan/Domain/Root/NScanMain.cs");

      var commits = repo.Branches[branchName].Commits.Reverse().ToArray();
      var analysisMetadata = new Dictionary<string, HashSet<string>>();
      var complexityPerPath = new Dictionary<string, double>();
      var pathsInTrunk = new List<string>();
      
      CollectPathsFrom(commits.Last().Tree, pathsInTrunk);
      CollectChangeRates(repo, commits, analysisMetadata);
      CollectComplexity(pathsInTrunk, complexityPerPath, repo.Info.WorkingDirectory);


      var trunkFiles = analysisMetadata.Where(am => pathsInTrunk.Contains(am.Key))
        .Select(x => new TrunkFile(x.Value.Count, x.Key, complexityPerPath[x.Key]));
      return trunkFiles;
    }

    private static void CollectComplexity(
      IEnumerable<string> pathsInTrunk, 
      IDictionary<string, double> complexityPerPath,
      string repositoryPath)
    {
      foreach (var path in pathsInTrunk)
      {
        CalculateComplexityFor(repositoryPath, path, complexityPerPath);
      }
    }

    private static void CalculateComplexityFor(
      string repositoryPath, 
      string path,
      IDictionary<string, double> complexityPerPath)
    {
      var complexity = ComplexityMetrics.CalculateComplexityFor(repositoryPath, path);
      complexityPerPath[path] = complexity;
    }

    private static void CollectChangeRates(
      IRepository repo, 
      IReadOnlyList<Commit> commits,
      Dictionary<string, HashSet<string>> analysisResults)
    {
      var treeVisitor = new CollectFileChangeRateFromCommitVisitor(analysisResults);
      TreeNavigation.Traverse(commits.First().Tree, treeVisitor);
      for (var i = 1; i < commits.Count; ++i)
      {
        var previousCommit = commits[i - 1];
        var currentCommit = commits[i];

        AnalyzeChanges(
          repo.Diff.Compare<TreeChanges>(previousCommit.Tree, currentCommit.Tree), 
          treeVisitor,
          currentCommit
          );
      }
    }

    private static void AnalyzeChanges(
      TreeChanges treeChanges,
      CollectFileChangeRateFromCommitVisitor treeVisitor, 
      Commit currentCommit)
    {
      foreach (var treeEntry in treeChanges)
      {
        switch (treeEntry.Status)
        {
          case ChangeKind.Unmodified:
            break;
          case ChangeKind.Added:
            treeVisitor.OnAdded(treeEntry, currentCommit);
            break;
          case ChangeKind.Deleted:
            break;
          case ChangeKind.Modified:
            treeVisitor.OnModified(treeEntry, currentCommit);
            break;
          case ChangeKind.Renamed:
            treeVisitor.OnRenamed(treeEntry, currentCommit);
            break;
          case ChangeKind.Copied:
            treeVisitor.OnCopied(treeEntry, currentCommit);
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

  /// <summary>
  /// Defines extensions used by <see cref="FileHistoryFixture"/>.
  /// </summary>
  internal static class FileHistoryFixtureExtensions
  {
    /// <summary>
    /// Gets the <see cref="Blob"/> instances contained in each <see cref="LogEntry"/>.
    /// </summary>
    /// <remarks>
    /// Use the <see cref="Enumerable.Distinct{TSource}(IEnumerable{TSource})"/> extension method
    /// to retrieve the changed blobs.
    /// </remarks>
    /// <param name="fileHistory">The file history.</param>
    /// <returns>The collection of <see cref="Blob"/> instances included in the file history.</returns>
    public static IEnumerable<Blob> Blobs(this IEnumerable<LogEntry> fileHistory)
    {
      return fileHistory.Select(entry => entry.Commit.Tree[entry.Path].Target).OfType<Blob>();
    }
  }
}