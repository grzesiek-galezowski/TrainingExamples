using System;
using System.IO;

namespace AtmaFileSystem
{
  public class FileName : IEquatable<FileName>
  {
    public bool Equals(FileName other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return string.Equals(_path, other._path);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((FileName) obj);
    }

    public override int GetHashCode()
    {
      return _path.GetHashCode();
    }

    public static bool operator ==(FileName left, FileName right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(FileName left, FileName right)
    {
      return !Equals(left, right);
    }

    private readonly string _path;

    public FileName(string path)
    {
      _path = path;
    }

    public static FileName Value(string path)
    {
      if (null == path)
      {
        throw new ArgumentNullException(path);
      }

      if (Path.GetFileName(path) != path)
      {
        throw new ArgumentException(path);
      }

      else return new FileName(path);
    }

    public override string ToString()
    {
      return _path;
    }

  }

  //TODO empty strings everywhere
}