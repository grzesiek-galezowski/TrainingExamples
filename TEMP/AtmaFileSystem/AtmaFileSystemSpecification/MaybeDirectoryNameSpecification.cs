using AtmaFileSystem;
using TddEbook.TddToolkit;
using Xunit;

namespace AtmaFileSystemSpecification
{
  public class MaybeDirectoryNameSpecification
  {
    [Fact]
    public void ShouldBehaveLikeValue2()
    {
      XAssert.IsValue<MaybeDirectoryPath>();
    }
  }
}