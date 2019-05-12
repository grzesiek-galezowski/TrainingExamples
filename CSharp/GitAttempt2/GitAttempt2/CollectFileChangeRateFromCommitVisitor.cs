using System;
using System.Collections.Generic;
using ApplicationLogic;
using LibGit2Sharp;
using static GitAttempt2.LibSpecificExtractions;

namespace GitAttempt2
{
  public interface ITreeVisitor
  {
    void OnBlob(TreeEntry treeEntry, Commit commit, Blob blob);
  }

  public class CollectFileChangeRateFromCommitVisitor : ITreeVisitor
  {
    private readonly Dictionary<string, ChangeLog> _commitsPerPath;

    public CollectFileChangeRateFromCommitVisitor(Dictionary<string, ChangeLog> commitsPerPath)
    {
      _commitsPerPath = commitsPerPath;
    }

    public void OnBlob(TreeEntry treeEntry, Commit commit, Blob blob)
    {
      if (!_commitsPerPath.ContainsKey(treeEntry.Path))
      {
        _commitsPerPath[treeEntry.Path] = new ChangeLog();
      }
      AddChange(treeEntry, blob, commit);
    }

    public void OnModified(TreeEntryChanges treeEntry, Commit currentCommit, Blob blob1)
    {
      AddChange(treeEntry, currentCommit, blob1);
    }

    private void AddChange(TreeEntry treeEntry, Blob blob, Commit currentCommit)
    {
      _commitsPerPath[treeEntry.Path].AddDataFrom(
        ChangeFactory.CreateChange(treeEntry.Path, blob.GetContentText(), currentCommit.Author.When));
    }

    private void AddChange(TreeEntryChanges treeEntry, Commit currentCommit, Blob blob)
    {
      _commitsPerPath[treeEntry.Path].AddDataFrom(
        ChangeFactory.CreateChange(treeEntry.Path, blob.GetContentText(), currentCommit.Author.When));
    }

    public void OnRenamed(TreeEntryChanges treeEntry)
    {
      _commitsPerPath[treeEntry.Path] = _commitsPerPath[treeEntry.OldPath];
    }


    public void OnCopied(TreeEntryChanges treeEntry, Commit currentCommit, Blob blob)
    {
      _commitsPerPath[treeEntry.Path] = new ChangeLog();
      AddChange(treeEntry, currentCommit, blob);
    }

    public void OnAdded(TreeEntryChanges treeEntry, Commit currentCommit, Blob blob)
    {
      _commitsPerPath[treeEntry.Path] = new ChangeLog();
      AddChange(treeEntry, currentCommit, blob);
    }
  }
}