using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;

namespace unit_tests_csharp.P04NSubstitute
{
  public class Ex06MockingRealClasses
  {
    [Test]  //please don't mock real classes!
    public void ShouldCreateMockOfRealClass()
    {
      //TODO uncomment later:// Substitute.ForPartsOf<CannotInstantiateThis>();
    }
  }

  public class CannotInstantiateThis
  {
    public CannotInstantiateThis()
    {
      throw new NotFiniteNumberException("a");
    }
  }
}
