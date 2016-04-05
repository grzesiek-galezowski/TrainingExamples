using NUnit.Framework;
using TddEbook.TddToolkit;

namespace unit_tests_csharp.P02Assertions
{
  public class Ex02SoftAssertions
  {
    [Test]
    public void ShouldSoftlyMatchWithNUnitApi()
    {
      Assert.That(12, Is.EqualTo(15).And.LessThan(13).Or.GreaterThan(11)); //TODO change both
    }

    [Test]
    public void ShouldSoftlyMatchWithTddToolkitApi()
    {
      XAssert.All(assert =>
      { //TODO change all
        assert.Equal(2, 2);
        assert.Equal(4, 4);
        assert.Equal(6, 6);
      });
    }

  }
}
