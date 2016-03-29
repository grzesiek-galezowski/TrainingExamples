using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace unit_tests_csharp
{
  public class Ex02CategoriesGroups
  {
    //TODO show how resharper groups this test
    [Test, Category("SlowTest"), Category("NotReallyAUnitTests")]
    [Category("JustFoolingAround")]
    public void ShouldDoWhatever()
    {
      //GIVEN

      //WHEN

      //THEN
    }
  }
}
