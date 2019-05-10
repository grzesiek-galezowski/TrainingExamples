using System;
using GitAttempt2;
using LibGit2Sharp;

static internal class TreeNavigation
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
          Traverse((Tree) treeEntry.Target, visitor);
          break;
        case TreeEntryTargetType.GitLink:
          throw new ArgumentException(treeEntry.Path);
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

  }
}