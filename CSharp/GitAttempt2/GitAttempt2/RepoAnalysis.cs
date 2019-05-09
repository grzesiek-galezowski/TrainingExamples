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
      var commits = repo.Branches[branchName].Commits.Reverse().ToArray();
      var analysisMetadata = new Dictionary<string, int>();
      var complexityPerPath = new Dictionary<string, double>();
      var pathsInTrunk = new List<string>();
      
      CollectPathsFrom(commits.Last().Tree, pathsInTrunk);
      CollectChangeRates(repo, commits, analysisMetadata);
      CollectComplexity(pathsInTrunk, complexityPerPath, repo.Info.WorkingDirectory);


      var trunkFiles = analysisMetadata.Where(am => pathsInTrunk.Contains(am.Key))
        .Select(x => new TrunkFile(x.Value, x.Key, complexityPerPath[x.Key]));
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
      var totalWhitespaces = 0;
      var currentIndentationLength = Maybe<int>.Nothing;
      var linesInFile = File.ReadLines(Path.Combine(repositoryPath, path)).ToArray();
      foreach (var line in linesInFile)
      {
        var lineIndentation = IndentationOf(line);
        if (ThereIsAny(lineIndentation) && IsBetter(lineIndentation, currentIndentationLength))
        {
          currentIndentationLength = lineIndentation.ToMaybe();
        }

        totalWhitespaces += lineIndentation;
      }

      complexityPerPath[path]
        = (TotalIndentations(totalWhitespaces, currentIndentationLength) + linesInFile.Length) / linesInFile.Length;
    }

    private static bool IsBetter(int lineIndentation, Maybe<int> currentIndentationLength)
    {
      return (!currentIndentationLength.HasValue || lineIndentation < currentIndentationLength.Value);
    }

    private static bool ThereIsAny(int lineIndentation)
    {
      return lineIndentation > 0;
    }

    private static double TotalIndentations(int totalWhitespaces, Maybe<int> indentationLength)
    {
      return 1d * totalWhitespaces / indentationLength.Select(i => i + 1).OrElse(1);
    }

    private static int IndentationOf(string line)
    {
      return line.TakeWhile(char.IsWhiteSpace).Count();
    }

    private static void CollectChangeRates(
      IRepository repo, 
      IReadOnlyList<Commit> commits,
      Dictionary<string, int> analysisResults)
    {
      Traverse(commits.First().Tree, new CommitsPerPathVisitor(analysisResults));
      for (var i = 1; i < commits.Count; ++i)
      {
        var previousCommit = commits[i - 1];
        var currentCommit = commits[i];

        AnalyzeChanges(repo.Diff.Compare<TreeChanges>(previousCommit.Tree, currentCommit.Tree), analysisResults);
      }
    }

    private static void Traverse(Tree tree, ITreeVisitor visitor)
    {
      foreach (var treeEntry in tree)
      {
        switch (treeEntry.TargetType)
        {
          case TreeEntryTargetType.Blob:
            visitor.OnBlob(treeEntry);
            break;
          case TreeEntryTargetType.Tree:
            Traverse((Tree) treeEntry.Target, visitor);
            break;
          case TreeEntryTargetType.GitLink:
            throw new ArgumentException(treeEntry.Path);
          default:
            throw new ArgumentOutOfRangeException();
        }
      }

    }

    private static void AnalyzeChanges(TreeChanges treeChanges, Dictionary<string, int> analysisResultPerPath)
    {
      foreach (var treeEntry in treeChanges)
      {
        switch (treeEntry.Status)
        {
          case ChangeKind.Unmodified:
            break;
          case ChangeKind.Added:
            analysisResultPerPath[treeEntry.Path] = 1;
            break;
          case ChangeKind.Deleted:
            break;
          case ChangeKind.Modified:
            analysisResultPerPath[treeEntry.Path]++;
            break;
          case ChangeKind.Renamed:
            analysisResultPerPath[treeEntry.Path] = analysisResultPerPath[treeEntry.OldPath];
            analysisResultPerPath[treeEntry.Path]++;
            break;
          case ChangeKind.Copied:
            analysisResultPerPath[treeEntry.Path] = 1;
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
}