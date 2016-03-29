using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TddEbook.TddToolkit;

namespace unit_tests_csharp.P02Assertions
{
  public class Ex02SoftAssertions
  {
    [Test]
    public void ShouldSoftlyMatchWithNUnitApi()
    {
      Assert.That(12, Is.EqualTo(15).And.LessThan(99).Or.GreaterThan(13));
    }

    [Test]
    public void ShouldSoftlyMatchWithTddToolkitApi()
    {
      XAssert.All(assert =>
      {
        assert.Equal(1, 2);
        assert.Equal(3, 4);
        assert.Equal(5, 6);
      });
    }

  }
}
