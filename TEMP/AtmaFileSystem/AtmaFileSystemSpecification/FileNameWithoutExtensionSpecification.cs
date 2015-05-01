using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtmaFileSystem;
using TddEbook.TddToolkit;
using Xunit;

namespace AtmaFileSystemSpecification
{
  public class FileNameWithoutExtensionSpecification
  {
    [Fact]
    public void ShouldBehaveLikeValue()
    {
      XAssert.IsValue<FileNameWithoutExtension>();
    }

    [Fact]
    public void ShouldAllowAccessingTheNameAsString()
    {
      //GIVEN
      var fileName = Any.String();
      var fileNameObject = new FileNameWithoutExtension(fileName);

      //WHEN
      var nameObtainedFromConversion = fileNameObject.ToString();

      //THEN
      Assert.Equal(fileName, nameObtainedFromConversion);
    }


  }
}
