using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TddEbook.TddToolkit;
using Xunit;

namespace AtmaFileSystemSpecification
{
  public class PathWithFileNameSpecification
  {

    [Fact]
    public void ShouldNotAllowToBeCreatedWithNullValue()
    {
      Assert.Throws<ArgumentNullException>(() => PathWithFileName.To(null));
    }

    [Fact]
    public void ShouldReturnNonNullPathToFileNameWhenCreatedWithWellFormedPathString()
    {
      Assert.NotNull(PathWithFileName.To(@"c:\\lolek\\lolki2.txt"));
    }

    [Fact]
    public void ShouldThrowArgumentExceptionWhenTryingToCreateInstanceWithNotWellFormedUri()
    {
      Assert.Throws<ArgumentException>(() => PathWithFileName.To(@"C:\?||\|\\|\"));
    }

    [Fact]
    public void ShouldBehaveLikeValueObject()
    {
      XAssert.IsValue<PathWithFileName>();
    }



  }

  public class PathWithFileName : IEquatable<PathWithFileName>
  {
    public bool Equals(PathWithFileName other)
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
      return Equals((PathWithFileName) obj);
    }

    public override int GetHashCode()
    {
      return _path.GetHashCode();
    }

    public static bool operator ==(PathWithFileName left, PathWithFileName right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(PathWithFileName left, PathWithFileName right)
    {
      return !Equals(left, right);
    }

    private readonly string _path;

    public PathWithFileName(string path)
    {
      _path = path;
    }

    public static PathWithFileName To(string path)
    {
      if (null == path)
      {
        throw new ArgumentNullException(path);
      }

      if (!Path.IsPathRooted(path))
      {
        throw new ArgumentException(path);
      }

      else return new PathWithFileName(path);
    }
  }
}
