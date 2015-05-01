using AtmaFileSystem;
using TddEbook.TddToolkit;
using Xunit;

namespace AtmaFileSystemSpecification
{
  public class FileExtensionSpecification
  {
    [Fact]
    public void ShouldBehaveLikeValue()
    {
      XAssert.IsValue<FileExtension>();
    }

    [Fact]
    public void ShouldAllowAccessingItsContentAsString()
    {
      //GIVEN
      var extensionString = ".zip";
      var extension = new FileExtension(extensionString);

      //WHEN
      var obtainedExtensionString = extension.ToString();

      //THEN
      Assert.Equal(extensionString, obtainedExtensionString);
    }


    //bug add factory method that validates everything, especially dot at beginning

  }
}
