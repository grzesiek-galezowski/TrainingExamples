using System.Collections.Generic;
using LibGit2Sharp;

namespace GitAttempt2
{
  public interface ITreeVisitor
  {
    void OnBlob(TreeEntry treeEntry);
  }

  public class CollectFileChangeRateFromCommitVisitor : ITreeVisitor
  {
    private readonly Dictionary<string, HashSet<string>> _commitsPerPath;

    public CollectFileChangeRateFromCommitVisitor(Dictionary<string, HashSet<string>> commitsPerPath)
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
          _commitsPerPath[treeEntry.Path] = new HashSet<string>();
        }

        _commitsPerPath[treeEntry.Path].Add(blob.GetContentText());
      }
    }

    public void OnModified(TreeEntryChanges treeEntry, Commit currentCommit)
    {
      var blob = BlobFrom(treeEntry, currentCommit);
      if (!blob.IsBinary)
      {
        _commitsPerPath[treeEntry.Path].Add(blob.GetContentText());
      }
    }

    public void OnRenamed(TreeEntryChanges treeEntry, Commit currentCommit)
    {
      var blob = BlobFrom(treeEntry, currentCommit);
      if (!blob.IsBinary)
      {
        _commitsPerPath[treeEntry.Path] = _commitsPerPath[treeEntry.OldPath];
        _commitsPerPath[treeEntry.Path].Add(blob.GetContentText());
      }
    }


    public void OnCopied(TreeEntryChanges treeEntry, Commit currentCommit)
    {
      var blob = BlobFrom(treeEntry, currentCommit);
      if (!blob.IsBinary)
      {
        _commitsPerPath[treeEntry.Path] = new HashSet<string>
        {
          blob.GetContentText()
        };
      }
    }

    public void OnAdded(TreeEntryChanges treeEntry, Commit currentCommit)
    {
      var blob = BlobFrom(treeEntry, currentCommit);
      if (!blob.IsBinary)
      {
        _commitsPerPath[treeEntry.Path] = new HashSet<string>
        {
          blob.GetContentText()
        };
      }
    }

    private static Blob BlobFrom(TreeEntryChanges treeEntry, Commit currentCommit)
    {
      return ((Blob)currentCommit[treeEntry.Path].Target);
    }
  }
}