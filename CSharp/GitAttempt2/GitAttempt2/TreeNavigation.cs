using System;
using LibGit2Sharp;

namespace GitAttempt2
{
  public static class TreeNavigation
  {
    public static void Traverse(Tree tree, ITreeVisitor visitor)
    {
      foreach (var treeEntry in tree)
      {
        switch (treeEntry.TargetType)
        {
          case TreeEntryTargetType.Blob:
            visitor.OnBlob(treeEntry);
            break;
          case TreeEntryTargetType.Tree:
            Traverse(treeEntry, visitor);
            break;
          case TreeEntryTargetType.GitLink:
            throw new ArgumentException(treeEntry.Path);
          default:
            throw new ArgumentOutOfRangeException();
        }
      }

    }

    private static void Traverse(TreeEntry treeEntry, ITreeVisitor visitor)
    {
      Traverse((Tree) treeEntry.Target, visitor);
    }
  }
}