using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationLogic;
using LibGit2Sharp;
using static GitAttempt2.LibSpecificExtractions;

namespace GitAttempt2
{
  public static class RepoAnalysis
  {
    public static AnalysisResult Analyze(string repositoryPath, string branchName)
    {
      //using var repo = new Repository(@"c:\Users\ftw637\Documents\GitHub\TrainingExamples\");
      //using var repo = new Repository(@"C:\Users\grzes\Documents\GitHub\nscan\");

      using var repo = new Repository(repositoryPath);
      var commits = repo.Branches[branchName].Commits.Reverse().ToArray();
      var analysisMetadata = new Dictionary<string, ChangeLog>();
      var pathsInTrunk = new List<string>();
      
      CollectPathsFrom(commits.Last().Tree, pathsInTrunk);
      CollectResults(repo, commits, analysisMetadata);

      var trunkFiles = analysisMetadata.Where(am => pathsInTrunk.Contains(am.Key)).Select(x => x.Value);
      var analysisResult = new AnalysisResult(trunkFiles, repo.Info.Path);
      return analysisResult;
    }

    private static void CollectResults(
      IRepository repo, 
      IReadOnlyList<Commit> commits,
      Dictionary<string, ChangeLog> analysisResults)
    {
      var treeVisitor = new CollectFileChangeRateFromCommitVisitor(analysisResults);
      TreeNavigation.Traverse(commits.First().Tree, commits.First(), treeVisitor);
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
          {
            var blob = BlobFrom(treeEntry, currentCommit);
            if (!blob.IsBinary)
            {
              treeVisitor.OnAdded(treeEntry.Path, blob.GetContentText(), currentCommit.Author.When);
            }
            break;
          }
          case ChangeKind.Deleted:
            break;
          case ChangeKind.Modified:
          {
            var blob = BlobFrom(treeEntry, currentCommit);
            if (!blob.IsBinary)
            {
              treeVisitor.OnModified(treeEntry.Path, blob.GetContentText(), currentCommit.Author.When);
            }

            break;
          }
          case ChangeKind.Renamed:
          {
            var blob = BlobFrom(treeEntry, currentCommit);
            if (!blob.IsBinary)
            {
              treeVisitor.OnRenamed(treeEntry.Path, treeEntry.OldPath);
            } 
            break;
          }
          case ChangeKind.Copied:
          {
            var blob = BlobFrom(treeEntry, currentCommit);
            if (!blob.IsBinary)
            {
              treeVisitor.OnCopied(treeEntry.Path, blob.GetContentText(), currentCommit.Author.When);
            }

            break;
          }

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
}