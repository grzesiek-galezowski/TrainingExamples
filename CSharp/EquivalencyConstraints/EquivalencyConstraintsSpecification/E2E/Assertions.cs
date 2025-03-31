using System.Text.RegularExpressions;
using EquivalencyConstraints.EquivalenceAssertions;

namespace EquivalencyConstraintsSpecification.E2E;

public static class Assertions
{
  public static void AssertEquivalencyAssertionFailedWhenComparing(
    object expected, object actual, string expectedInMessage, string actualInMessage)
  {
    var equivalenceException =
      Assert.Throws<AssertionException>(() => Assert.That(
        actual,
        Should.BeEquivalentTo(expected)));
    Assert.That(Regex.Replace(equivalenceException.Message, @"\s+", " ").Trim(), Is.EqualTo(
        "Assert.That(actual, Should.BeEquivalentTo(expected)) " +
        $"Expected: equivalent to {expectedInMessage} " +
        $"But was: <{actualInMessage}>"),
      equivalenceException.Message);
  }
}