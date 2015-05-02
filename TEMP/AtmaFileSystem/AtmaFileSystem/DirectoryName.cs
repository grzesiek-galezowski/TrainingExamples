using System;

namespace AtmaFileSystem
{
  public class DirectoryName : IEquatable<DirectoryName>
  {
    public bool Equals(DirectoryName other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return string.Equals(_directoryName, other._directoryName);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((DirectoryName) obj);
    }

    public override int GetHashCode()
    {
      return (_directoryName != null ? _directoryName.GetHashCode() : 0);
    }

    public static bool operator ==(DirectoryName left, DirectoryName right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(DirectoryName left, DirectoryName right)
    {
      return !Equals(left, right);
    }

    private readonly string _directoryName;

    public DirectoryName(string directoryName)
    {
      _directoryName = directoryName;
    }

    public override string ToString()
    {
      return _directoryName;
    }
  }
}