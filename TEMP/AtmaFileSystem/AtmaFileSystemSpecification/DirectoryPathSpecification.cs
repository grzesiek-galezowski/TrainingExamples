using System;
using System.IO;
using AtmaFileSystem;
using TddEbook.TddToolkit;
using Xunit;

namespace AtmaFileSystemSpecification
{
  public class DirectoryPathSpecification
  {

    [Fact]
    public void ShouldNotAllowToBeCreatedWithNullValue()
    {
      Assert.Throws<ArgumentNullException>(() => DirectoryPath.To(null));
    }

    [Fact]
    public void ShouldReturnNonNullPathToFileNameWhenCreatedWithWellFormedPathString()
    {
      Assert.NotNull(DirectoryPath.To(@"c:\lolek\"));
    }

    [Fact]
    public void ShouldThrowArgumentExceptionWhenTryingToCreateInstanceWithNotWellFormedUri()
    {
      Assert.Throws<ArgumentException>(() => DirectoryPath.To(@"C:\?||\|\\|\"));
    }

    [Fact]
    public void ShouldBehaveLikeValueObject()
    {
      XAssert.IsValue<DirectoryPath>();
    }

    [Fact]
    public void ShouldReturnTheStringItWasCreatedWithWhenConvertedToString()
    {
      //GIVEN
      var initialValue = Any.String();
      var path = new DirectoryPath(initialValue);

      //WHEN
      var convertedToString = path.ToString();

      //THEN
      Assert.Equal(initialValue, convertedToString);
    }

    [Fact]
    public void ShouldAllowUsingTheAdditionOperatorToConcatenateFileName()
    {
      //GIVEN
      var path = Any.Instance<DirectoryPath>();
      var fileName = Any.Instance<FileName>();
      PathWithFileName pathWithFileName = path + fileName;

      //WHEN
      var convertedToString = pathWithFileName.ToString();

      //THEN
      Assert.Equal(Path.Combine(path.ToString(), fileName.ToString()), convertedToString);
    }


    [Fact]
    public void ShouldBeConvertibleToDirectoryInfo()
    {
      //GIVEN
      var directoryPath = DirectoryPath.To(@"C:\lolek\");

      //WHEN
      DirectoryInfo directoryInfo = directoryPath.Info();

      //THEN
      Assert.Equal(directoryInfo.FullName, directoryPath.ToString());
    }

    [Fact]
    public void ShouldAllowGettingPathRoot()
    {
      //GIVEN
      var pathString = @"C:\lolek\";
      var dir = DirectoryPath.To(pathString);

      //WHEN
      DirectoryPath root = dir.Root();

      //THEN
      Assert.Equal(new DirectoryPath(Path.GetPathRoot(pathString)), root);
    }

    [Theory, 
      InlineData(@"C:\parent\child\", @"C:\parent"),
      InlineData(@"C:\parent\", @"C:\")]
    public void ShouldAllowGettingProperParentDirectoryWhenItExists(string input, string expected)
    {
      //GIVEN
      var dir = DirectoryPath.To(input);

      //WHEN
      var parent = dir.Parent();

      //THEN
      Assert.True(parent.Found);
      Assert.Equal(new DirectoryPath(expected), parent.Value());
    }

    [Fact]
    public void ShouldProduceParentWithoutValueThatThrowsOnAccessWhenThereIsNoParentInPath()
    {
      //GIVEN
      const string pathString = @"C:\";
      var dir = DirectoryPath.To(pathString);

      //WHEN
      var parent = dir.Parent();

      //THEN
      Assert.False(parent.Found);
      Assert.Throws<InvalidOperationException>(() => parent.Value());
    }

    [Theory,
      InlineData(@"F:\Segment1\Segment2\", "Segment2"),
      InlineData(@"F:\Segment1\", "Segment1"),
      InlineData(@"F:\", @"F:\"), //bug is this OK? Directory names will be added, so "F:\" + "F:\"... maybe introduce a root element?
      ]
    public void ShouldAllowGettingTheNameOfCurrentDirectory(string fullPath, string expectedDirectoryName)
    {
      //GIVEN
      var directoryPath = new DirectoryPath(fullPath);

      //WHEN
      DirectoryName dirName = directoryPath.DirectoryName();
      
      //THEN
      Assert.Equal(expectedDirectoryName, dirName.ToString());
    }


  //bug directoryName should be value


  }
}
