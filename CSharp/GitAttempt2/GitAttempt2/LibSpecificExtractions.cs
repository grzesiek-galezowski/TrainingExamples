using LibGit2Sharp;

namespace GitAttempt2
{
  public static class LibSpecificExtractions
  {
    public static Blob BlobFrom(TreeEntryChanges treeEntry, Commit currentCommit)
    {
      return ((Blob)currentCommit[treeEntry.Path].Target);
    }
  }
}