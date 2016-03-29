using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace unit_tests_csharp.P02Assertions
{
  public class Ex03ExceptionAssertions
  {
    [Test]
    public void ShouldShowThrownAssertions()
    {
      var e = Assert.Throws<FormatException>(() => DoSomethingThatThrows());
      Assert.AreEqual("Tralala", e.Message);
    }

    private void DoSomethingThatThrows()
    {
      throw new FormatException("Tralala");
    }

    [Test]
    public void ShouldShowNotThrownAssertions()
    {
      Assert.DoesNotThrow(() => DoSomethingThatDoesNotThrow());
    }

    private void DoSomethingThatDoesNotThrow()
    {
      
    }

  }
}
