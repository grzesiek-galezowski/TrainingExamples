using NUnit.Framework;

namespace DealingWithNull
{
  public class MaybeSpecification
  {
    [Test]
    public void ShouldBEHAVIOR()
    {
      var maybeNothing = ReturnNull();
      Assert.AreEqual(false, maybeNothing.HasValue);
      Assert.AreEqual(default(Maybe<string>), maybeNothing);
      foreach (var value in maybeNothing)
      {
        Assert.Fail("Should not have any value");
      }

      var maybeString = ReturnEmptyString();
      Assert.AreEqual(true, maybeString.HasValue);
      Assert.AreNotEqual(default(Maybe<string>), maybeString);
    }

    private static Maybe<string> ReturnNull()
    {
      return null;
    }

    private static Maybe<string> ReturnEmptyString()
    {
      return string.Empty;
    }
  }
}