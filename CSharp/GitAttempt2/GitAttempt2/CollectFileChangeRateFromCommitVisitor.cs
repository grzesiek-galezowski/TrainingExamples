using System;
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
    private readonly Dictionary<string, int> _commitsPerPath;

    public CollectFileChangeRateFromCommitVisitor(Dictionary<string, int> commitsPerPath)
    {
      _commitsPerPath = commitsPerPath;
    }

    public void OnBlob(TreeEntry treeEntry)
    {
      if (!_commitsPerPath.ContainsKey(treeEntry.Path))
      {
        _commitsPerPath[treeEntry.Path] = 0;
      }

      _commitsPerPath[treeEntry.Path]++;
    }
  }
}