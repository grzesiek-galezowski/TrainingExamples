using System;

namespace AtmaFileSystem
{
  public class FileExtension //bug implement factory methods
    : IEquatable<FileExtension>
  {
    public bool Equals(FileExtension other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return string.Equals(_extension, other._extension);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((FileExtension) obj);
    }

    public override int GetHashCode()
    {
      return (_extension != null ? _extension.GetHashCode() : 0);
    }

    public static bool operator ==(FileExtension left, FileExtension right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(FileExtension left, FileExtension right)
    {
      return !Equals(left, right);
    }

    private readonly string _extension;

    public FileExtension(string extension)
    {
      this._extension = extension;
    }

    public override string ToString()
    {
      return _extension;
    }
  }
}