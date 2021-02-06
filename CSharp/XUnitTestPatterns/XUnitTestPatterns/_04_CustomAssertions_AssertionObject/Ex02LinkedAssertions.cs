using FluentAssertions;
using NUnit.Framework;

namespace XUnitTestPatterns._04_CustomAssertions_AssertionObject
{
  public class Ex02LinkedAssertions
  {
    [Test]
    public void Trololololo1()
    {
      1.Should().Be(1);
      1.Should().BeGreaterThan(0);
      1.Should().BeLessThan(2);
      1.Should().Be(2);
    }

    [Test]
    public void Trololololo2()
    {
      1.Should().Be(1)
        .And.BeGreaterThan(0)
        .And.BeLessThan(2)
        .And.Be(2);
    }
  }
}