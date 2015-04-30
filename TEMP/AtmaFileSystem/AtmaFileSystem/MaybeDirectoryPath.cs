using System;
using CallMeMaybe;

namespace AtmaFileSystem
{
  public class MaybeDirectoryPath : IEquatable<MaybeDirectoryPath>
  {
    private readonly Maybe<DirectoryPath> _maybeDirectoryPath;

    public MaybeDirectoryPath(string value)
    {
      _maybeDirectoryPath = value == null ? Maybe.Not : new Maybe<DirectoryPath>(new DirectoryPath(value));
    }

    public DirectoryPath Value()
    {
      return _maybeDirectoryPath.Single();
    }

    public bool Found
    {
      get { return _maybeDirectoryPath.HasValue; }
    }

    public bool Equals(MaybeDirectoryPath other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return _maybeDirectoryPath.Equals(other._maybeDirectoryPath);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((MaybeDirectoryPath)obj);
    }

    public override int GetHashCode()
    {
      return _maybeDirectoryPath.GetHashCode();
    }

    public static bool operator ==(MaybeDirectoryPath left, MaybeDirectoryPath right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(MaybeDirectoryPath left, MaybeDirectoryPath right)
    {
      return !Equals(left, right);
    }
  }
}