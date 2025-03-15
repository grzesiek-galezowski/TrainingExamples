using EquivalencyConstraints.EquivalenceAssertions;
using EquivalencyConstraintsSpecification.Fixture;
using System.Text.RegularExpressions;

namespace EquivalencyConstraintsSpecification;

public class SimpleEquivalencySpecification
{
  [Test]
  public void ShouldPassForObjectsCreatedTheSameWay()
  {
    Assert.That(1, Should.BeEquivalentTo(1));
    Assert.That(new object(), Should.BeEquivalentTo(new object()));
    Assert.That(
      new Person
      {
        Age = 1,
        Name = "Zenek"
      },
      Should.BeEquivalentTo(new Person
      {
        Age = 1,
        Name = "Zenek"
      }));
  }

  [Test]
  public void ShouldFailForDifferentObjects()
  {
      AssertEquivalencyAssertionFailedWhenComparing(2, 1, "2", "1");
      AssertEquivalencyAssertionFailedWhenComparing(
        new Person
        {
          Age = 1,
          Name = "Zenek"
        },
        new Person
        {
          Age = 2,
          Name = "Zenek"
        },
        "{ Name: Zenek, Age: 1 }",
        "{ Name: Zenek, Age: 2 }");
      AssertEquivalencyAssertionFailedWhenComparing(new Person
      {
        Age = 1,
        Name = "Zenek2"
      },
      new Person
      {
        Age = 1,
        Name = "Zenek"
      },
      "{ Name: Zenek2, Age: 1 }",
      "{ Name: Zenek, Age: 1 }");

      AssertEquivalencyAssertionFailedWhenComparing(new Company()
        {
          Director = new Person()
          {
            Age = 2,
            Name = "Zenek"
          }
        },
        new Company()
        {
          Director = new Person()
          {
            Age = 1,
            Name = "Zenek"
          }
        },
        "{ Director: { Name: Zenek, Age: 2 } }",
        "{ Director: { Name: Zenek, Age: 1 } }");
  }

  private static void AssertEquivalencyAssertionFailedWhenComparing(
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