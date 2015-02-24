using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using TddEbook.TddToolkit;

namespace TDDWithMocks
{
    public class Class1
    {
      [Test]
      public void CompositionRoot()
      {
        //TODO place composition here
      }

      [Test]
      public void ShouldBEHAVIOR()
      {
        //GIVEN
        var l1 = Substitute.For<Lolek>();
        var l2 = Any.Instance<Lolek>();

        //WHEN

        //THEN
      }

      public interface Lolek
      {
        
      }
    }
}
