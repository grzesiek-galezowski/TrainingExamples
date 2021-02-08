using System;
using FluentAssertions;
using NUnit.Framework;

namespace XUnitTestPatterns._05_AssertionObject
{
  public class _04_ExceptionAssertions
  {
    [Test]
    public void ShouldThrowWhenValueIsNegative1()
    {
      //GIVEN
      var businessAssertion = new NumberAssertion();

      //WHEN - THEN
      var caughtException = Assert.Throws<InvalidOperationException>(
        () => businessAssertion.ApplyTo(-1));
      Assert.IsNull(caughtException.InnerException);
      Assert.AreEqual("Trolololo", caughtException.Message);
    }

    [Test]
    public void ShouldThrowWhenValueIsNegative2()
    {
      //GIVEN
      var businessAssertion = new NumberAssertion();

      //WHEN - THEN
      businessAssertion.Invoking(a => a.ApplyTo(-1))
        .Should().ThrowExactly<InvalidOperationException>()
        .WithMessage("Trolololo")
        .Which.InnerException.Should().BeNull();
    }

    [Test]
    public void ShouldNotThrowWhenValueIsNonNegative1()
    {
      //GIVEN
      var businessAssertion = new NumberAssertion();

      //WHEN - THEN
      Assert.DoesNotThrow(() => businessAssertion.ApplyTo(0));
    }

    [Test]
    public void ShouldNotThrowWhenValueIsNonNegative2()
    {
      //GIVEN
      var businessAssertion = new NumberAssertion();

      //WHEN - THEN
      businessAssertion.Invoking(a => a.ApplyTo(0)).Should().NotThrow();
    }
  }
}