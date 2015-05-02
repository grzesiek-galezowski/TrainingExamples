using System;
using System.IO;

namespace AtmaFileSystem
{
  public class DirectoryPath : IEquatable<DirectoryPath>
  {
    private readonly string _path;
    private readonly DirectoryInfo _directoryInfo;

    public DirectoryPath(string path)
    {
      _path = path;
      _directoryInfo = new DirectoryInfo(_path);
    }

    public static DirectoryPath To(string path)
    {
      if (null == path)
      {
        throw new ArgumentNullException(path);
      }

      if (!Path.IsPathRooted(path))
      {
        throw new ArgumentException(path);
      }

      else return new DirectoryPath(path);
    }

    public override string ToString()
    {
      return _path;
    }

    public DirectoryInfo Info()
    {
      return _directoryInfo;
    }

    public Maybe<DirectoryPath> Parent()
    {
      var directoryName = _directoryInfo.Parent;
      return AsMaybe(directoryName);
    }

    private static Maybe<DirectoryPath> AsMaybe(DirectoryInfo directoryName)
    {
      return directoryName != null ? Maybe.Wrap(new DirectoryPath(directoryName.FullName)) : null;
    }

    public DirectoryPath Root()
    {
      return new DirectoryPath(Path.GetPathRoot(_path));
    }

    public static PathWithFileName operator +(DirectoryPath path, FileName fileName)
    {
      return PathWithFileName.From(path, fileName);
    }

    public bool Equals(DirectoryPath other)
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
      return Equals((DirectoryPath)obj);
    }

    public override int GetHashCode()
    {
      return _path.GetHashCode();
    }

    public static bool operator ==(DirectoryPath left, DirectoryPath right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(DirectoryPath left, DirectoryPath right)
    {
      return !Equals(left, right);
    }

    public DirectoryName DirectoryName()
    {
      return new DirectoryName(_directoryInfo.Name);
    }
  }
}