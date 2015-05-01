using System;
using CallMeMaybe;

namespace AtmaFileSystem
{
  public class MaybeFileExtension
    : IEquatable<MaybeFileExtension>
  {
    private readonly Maybe<FileExtension> _maybeFileExtension;

    public MaybeFileExtension(Maybe<FileExtension> value)
    {
      _maybeFileExtension = value;
    }

    public FileExtension Value()
    {
      return _maybeFileExtension.Single();
    }

    public bool Found
    {
      get { return _maybeFileExtension.HasValue; }
    }

    public bool Equals(MaybeFileExtension other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return _maybeFileExtension.Equals(other._maybeFileExtension);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((MaybeFileExtension) obj);
    }

    public override int GetHashCode()
    {
      return _maybeFileExtension.GetHashCode();
    }

    public static bool operator ==(MaybeFileExtension left, MaybeFileExtension right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(MaybeFileExtension left, MaybeFileExtension right)
    {
      return !Equals(left, right);
    }


  }
}