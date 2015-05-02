using System;
using System.IO;

namespace AtmaFileSystem
{
  public class FileName : IEquatable<FileName>
  {
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
      return Equals((FileName)obj);
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

    public Maybe<FileExtension> Extension()
    {
      var extension = Path.GetExtension(_path);
      return AsMaybe(extension);
    }

    private static Maybe<FileExtension> AsMaybe(string extension)
    {
      return extension == string.Empty ? Maybe<FileExtension>.Not : Maybe.Wrap(new FileExtension(extension));
    }

    public FileNameWithoutExtension WithoutExtension()
    {
      return new FileNameWithoutExtension(Path.GetFileNameWithoutExtension(_path));
    }
  }

  //TODO implement ValueOr()

  //TODO validate against using empty strings everywhere
  //TODO check Pri.LongPath to handle long paths
  //TODO implement file system
}