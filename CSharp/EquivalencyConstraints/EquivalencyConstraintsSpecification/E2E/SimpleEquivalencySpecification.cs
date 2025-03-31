using EquivalencyConstraints.EquivalenceAssertions;
using EquivalencyConstraintsSpecification.E2E.Fixture;

namespace EquivalencyConstraintsSpecification.E2E;

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
    Assertions.AssertEquivalencyAssertionFailedWhenComparing(2, 1, "2", "1");
    Assertions.AssertEquivalencyAssertionFailedWhenComparing(
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
    Assertions.AssertEquivalencyAssertionFailedWhenComparing(new Person
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

    Assertions.AssertEquivalencyAssertionFailedWhenComparing(new Company()
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
}