using FluentAssertions;
using NUnit.Framework;

namespace XUnitTestPatterns._04_CustomAssertions_AssertionObject
{
  public class Ex03FailureMessagesAndDescriptions
  {
    [Test]
    public void Trolololo()
    {
      Assert.AreEqual(2, 2, "an integer should be equal to itself 1");
      Assert.AreEqual("2", "2", "a string should be equal to itself 1");

      2.Should().Be(2, "an integer should be equal to itself 2");
      "2".Should().Be("2", "a string should be equal to itself 2");
    }
  }
}