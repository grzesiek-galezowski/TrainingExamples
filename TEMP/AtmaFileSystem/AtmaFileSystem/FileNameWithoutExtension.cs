using System;
using System.IO;

namespace AtmaFileSystem
{
  public class FileNameWithoutExtension : IEquatable<FileNameWithoutExtension>
  {
    private readonly string _fileNameWithoutExtensionString;

    public FileNameWithoutExtension(string fileNameWithoutExtensionString)
    {
      _fileNameWithoutExtensionString = fileNameWithoutExtensionString;
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((FileNameWithoutExtension) obj);
    }

    public bool Equals(FileNameWithoutExtension other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return string.Equals(_fileNameWithoutExtensionString, other._fileNameWithoutExtensionString);
    }

    public override int GetHashCode()
    {
      return _fileNameWithoutExtensionString.GetHashCode();
    }

    public static bool operator ==(FileNameWithoutExtension left, FileNameWithoutExtension right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(FileNameWithoutExtension left, FileNameWithoutExtension right)
    {
      return !Equals(left, right);
    }

    public override string ToString()
    {
      return _fileNameWithoutExtensionString;
    }

    public FileName With(FileExtension extension)
    {
      return new FileName(Path.ChangeExtension(_fileNameWithoutExtensionString, extension.ToString()));
    }
  }
}