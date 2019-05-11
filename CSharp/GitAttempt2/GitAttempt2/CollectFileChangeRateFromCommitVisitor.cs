using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using LibGit2Sharp;

namespace GitAttempt2
{
  public interface ITreeVisitor
  {
    void OnBlob(TreeEntry treeEntry, Commit commit);
  }

  public class CollectFileChangeRateFromCommitVisitor : ITreeVisitor
  {
    private readonly Dictionary<string, AnalysisLog> _commitsPerPath;

    public CollectFileChangeRateFromCommitVisitor(Dictionary<string, AnalysisLog> commitsPerPath)
    {
      _commitsPerPath = commitsPerPath;
    }

    public void OnBlob(TreeEntry treeEntry, Commit commit)
    {
      var blob = (Blob) treeEntry.Target;
      if (!blob.IsBinary)
      {
        if (!_commitsPerPath.ContainsKey(treeEntry.Path))
        {
          _commitsPerPath[treeEntry.Path] = new AnalysisLog();
        }

        _commitsPerPath[treeEntry.Path].AddDataFrom(blob, commit.Author.When);
      }
    }

    public void OnModified(TreeEntryChanges treeEntry, Commit currentCommit)
    {
      var blob = BlobFrom(treeEntry, currentCommit);
      if (!blob.IsBinary)
      {
        _commitsPerPath[treeEntry.Path].AddDataFrom(blob, currentCommit.Author.When);
      }
    }

    public void OnRenamed(TreeEntryChanges treeEntry, Commit currentCommit)
    {
      var blob = BlobFrom(treeEntry, currentCommit);
      if (!blob.IsBinary)
      {
        _commitsPerPath[treeEntry.Path] = _commitsPerPath[treeEntry.OldPath];
        _commitsPerPath[treeEntry.Path].AddDataFrom(blob, currentCommit.Author.When);
      }
    }


    public void OnCopied(TreeEntryChanges treeEntry, Commit currentCommit)
    {
      var blob = BlobFrom(treeEntry, currentCommit);
      if (!blob.IsBinary)
      {
        _commitsPerPath[treeEntry.Path] = new AnalysisLog();
        _commitsPerPath[treeEntry.Path].AddDataFrom(blob, currentCommit.Author.When);
      }
    }

    public void OnAdded(TreeEntryChanges treeEntry, Commit currentCommit)
    {
      var blob = BlobFrom(treeEntry, currentCommit);
      if (!blob.IsBinary)
      {
        _commitsPerPath[treeEntry.Path] = new AnalysisLog();
        _commitsPerPath[treeEntry.Path].AddDataFrom(blob, currentCommit.Author.When);
      }
    }

    private static Blob BlobFrom(TreeEntryChanges treeEntry, Commit currentCommit)
    {
      return ((Blob)currentCommit[treeEntry.Path].Target);
    }
  }

  public class AnalysisLog
  {
    private readonly List<Change> _results = new List<Change>();

    public IReadOnlyList<Change> Results => _results;

    public void AddDataFrom(Blob blob, DateTimeOffset changeDate)
    {
      var change = new Change(
        blob.GetContentText(), 
        ComplexityMetrics.CalculateComplexityFor(Regex.Split(blob.GetContentText(), @"\r\n|\r|\n")),
        changeDate);
      
      if (!_results.Contains(change))
      {
        _results.Add(change);
      }
    }
  }
}