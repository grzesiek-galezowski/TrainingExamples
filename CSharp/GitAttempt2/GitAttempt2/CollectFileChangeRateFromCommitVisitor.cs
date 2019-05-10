using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using LibGit2Sharp;

namespace GitAttempt2
{
  public interface ITreeVisitor
  {
    void OnBlob(TreeEntry treeEntry);
  }

  public class CollectFileChangeRateFromCommitVisitor : ITreeVisitor
  {
    private readonly Dictionary<string, AnalysisLog> _commitsPerPath;

    public CollectFileChangeRateFromCommitVisitor(Dictionary<string, AnalysisLog> commitsPerPath)
    {
      _commitsPerPath = commitsPerPath;
    }

    public void OnBlob(TreeEntry treeEntry)
    {
      var blob = (Blob) treeEntry.Target;
      if (!blob.IsBinary)
      {
        if (!_commitsPerPath.ContainsKey(treeEntry.Path))
        {
          _commitsPerPath[treeEntry.Path] = new AnalysisLog();
        }

        _commitsPerPath[treeEntry.Path].AddDataFrom(blob);
      }
    }

    public void OnModified(TreeEntryChanges treeEntry, Commit currentCommit)
    {
      var blob = BlobFrom(treeEntry, currentCommit);
      if (!blob.IsBinary)
      {
        _commitsPerPath[treeEntry.Path].AddDataFrom(blob);
      }
    }

    public void OnRenamed(TreeEntryChanges treeEntry, Commit currentCommit)
    {
      var blob = BlobFrom(treeEntry, currentCommit);
      if (!blob.IsBinary)
      {
        _commitsPerPath[treeEntry.Path] = _commitsPerPath[treeEntry.OldPath];
        _commitsPerPath[treeEntry.Path].AddDataFrom(blob);
      }
    }


    public void OnCopied(TreeEntryChanges treeEntry, Commit currentCommit)
    {
      var blob = BlobFrom(treeEntry, currentCommit);
      if (!blob.IsBinary)
      {
        _commitsPerPath[treeEntry.Path] = new AnalysisLog();
        _commitsPerPath[treeEntry.Path].AddDataFrom(blob);
      }
    }

    public void OnAdded(TreeEntryChanges treeEntry, Commit currentCommit)
    {
      var blob = BlobFrom(treeEntry, currentCommit);
      if (!blob.IsBinary)
      {
        _commitsPerPath[treeEntry.Path] = new AnalysisLog();
        _commitsPerPath[treeEntry.Path].AddDataFrom(blob);
      }
    }

    private static Blob BlobFrom(TreeEntryChanges treeEntry, Commit currentCommit)
    {
      return ((Blob)currentCommit[treeEntry.Path].Target);
    }
  }

  public class AnalysisLog
  {
    private List<AnalysisResult> _results = new List<AnalysisResult>();

    public IReadOnlyList<AnalysisResult> Results => _results;

    public void AddDataFrom(Blob blob)
    {
      var analysisResult = new AnalysisResult(
        blob.GetContentText(), 
        ComplexityMetrics.CalculateComplexityFor(Regex.Split(blob.GetContentText(), @"\r\n|\r|\n")));
      
      if (!_results.Contains(analysisResult))
      {
        _results.Add(analysisResult);
      }
    }
  }
}