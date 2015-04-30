using System;
using System.IO;
using AtmaFileSystem;
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
    public void ShouldReturnNonNullFileNameWhenCreatedWithWellFormedPathString()
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

    [Fact]
    public void ShouldReturnTheStringItWasCreatedWithWhenConvertedToString()
    {
      //GIVEN
      var initialValue = Any.String();
      var path = new PathWithFileName(initialValue);

      //WHEN
      var convertedToString = path.ToString();

      //THEN
      Assert.Equal(initialValue, convertedToString);
    }

    [Fact]
    public void ShouldReturnCombinedPathOfDirectoryPathAndFileNameItWasCreatedWithWhenConvertedToString()
    {
      //GIVEN
      var dirPath = Any.Instance<DirectoryPath>();
      var fileName = Any.Instance<FileName>();
      var path = PathWithFileName.From(dirPath, fileName);

      //WHEN
      var convertedToString = path.ToString();

      //THEN
      Assert.Equal(Path.Combine(dirPath.ToString(), fileName.ToString()), convertedToString);
    }



  }
}
