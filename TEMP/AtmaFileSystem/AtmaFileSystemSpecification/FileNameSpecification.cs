using System;
using AtmaFileSystem;
using TddEbook.TddToolkit;
using Xunit;

namespace AtmaFileSystemSpecification
{
  public class FileNameSpecification
  {
    [Fact]
    public void ShouldNotAllowToBeCreatedWithNullValue()
    {
      Assert.Throws<ArgumentNullException>(() => FileName.Value(null));
    }

    [Fact]
    public void ShouldThrowExceptionWhenCreatedWithStringContainingMoreThanJustAFileName()
    {
      Assert.Throws<ArgumentException>(() => FileName.Value(@"c:\\lolek\\lolki2.txt"));
    }

    [Fact]
    public void ShouldReturnNonNullFileNameWhenCreatedWithWellFormedPathString()
    {
      Assert.NotNull(FileName.Value("lolki2.txt"));
    }

    [Fact]
    public void ShouldBehaveLikeValueObject()
    {
      XAssert.IsValue<FileName>();
    }

    [Fact]
    public void ShouldReturnTheStringItWasCreatedWithWhenConvertedToString()
    {
      //GIVEN
      var initialValue = Any.String();
      var path = new FileName(initialValue);

      //WHEN
      var convertedToString = path.ToString();

      //THEN
      Assert.Equal(initialValue, convertedToString);
    }


/* TODO    
Public methodStatic memberSupported by the XNA Framework GetExtension  Returns the extension of the specified path string.
Public methodStatic memberSupported by the XNA Framework GetFileName Returns the file name and extension of the specified path string.
Public methodStatic memberSupported by the XNA Framework GetFileNameWithoutExtension Returns the file name of the specified path string without the extension.
Public methodStatic member GetRandomFileName Returns a random folder name or file name.
Public methodStatic member GetTempFileName Creates a uniquely named, zero-byte temporary file on disk and returns the full path of that file.
Public methodStatic member GetTempPath Returns the path of the current user's temporary folder.
Public methodStatic memberSupported by the XNA Framework HasExtension  Determines whether a path includes a file name extension.
Public methodStatic memberSupported by the XNA Framework IsPathRooted
*/
  }
}
