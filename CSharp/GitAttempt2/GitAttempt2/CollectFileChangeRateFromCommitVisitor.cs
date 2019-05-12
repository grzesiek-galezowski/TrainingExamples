using System;
using System.Collections.Generic;
using ApplicationLogic;
using LibGit2Sharp;
using static GitAttempt2.LibSpecificExtractions;

namespace GitAttempt2
{
  public interface ITreeVisitor
  {
    void OnBlob(TreeEntry treeEntry, Commit commit);
  }

  public class CollectFileChangeRateFromCommitVisitor : ITreeVisitor
  {
    private readonly Dictionary<string, ChangeLog> _commitsPerPath;

    public CollectFileChangeRateFromCommitVisitor(Dictionary<string, ChangeLog> commitsPerPath)
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
          _commitsPerPath[treeEntry.Path] = new ChangeLog();
        }

        DateTimeOffset changeDate = commit.Author.When;
        _commitsPerPath[treeEntry.Path].AddDataFrom(ChangeFactory.CreateChange(treeEntry.Path, blob, changeDate));
      }
    }

    public void OnModified(TreeEntryChanges treeEntry, Commit currentCommit)
    {
      var blob = BlobFrom(treeEntry, currentCommit);
      if (!blob.IsBinary)
      {
        DateTimeOffset changeDate = currentCommit.Author.When;
        _commitsPerPath[treeEntry.Path].AddDataFrom(ChangeFactory.CreateChange(treeEntry.Path, blob, changeDate));
      }
    }

    public void OnRenamed(TreeEntryChanges treeEntry, Commit currentCommit)
    {
      var blob = BlobFrom(treeEntry, currentCommit);
      if (!blob.IsBinary)
      {
        _commitsPerPath[treeEntry.Path] = _commitsPerPath[treeEntry.OldPath];
        DateTimeOffset changeDate = currentCommit.Author.When;
        _commitsPerPath[treeEntry.Path].AddDataFrom(ChangeFactory.CreateChange(treeEntry.Path, blob, changeDate));
      }
    }


    public void OnCopied(TreeEntryChanges treeEntry, Commit currentCommit)
    {
      var blob = BlobFrom(treeEntry, currentCommit);
      if (!blob.IsBinary)
      {
        _commitsPerPath[treeEntry.Path] = new ChangeLog();
        _commitsPerPath[treeEntry.Path].AddDataFrom(ChangeFactory.CreateChange(treeEntry.Path, blob, currentCommit.Author.When));
      }
    }

    public void OnAdded(TreeEntryChanges treeEntry, Commit currentCommit)
    {
      var blob = BlobFrom(treeEntry, currentCommit);
      if (!blob.IsBinary)
      {
        _commitsPerPath[treeEntry.Path] = new ChangeLog();
        DateTimeOffset changeDate = currentCommit.Author.When;
        _commitsPerPath[treeEntry.Path].AddDataFrom(ChangeFactory.CreateChange(treeEntry.Path, blob, changeDate));
      }
    }
  }
}