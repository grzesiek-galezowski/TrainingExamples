using EquivalencyConstraints.EquivalenceAssertions;
using EquivalencyConstraintsSpecification.Fixture;

namespace EquivalencyConstraintsSpecification;

public class EquivalencySpecification
{



  [Test]
  public void NestedObjects_ShouldBeEquivalent()
  {
    var expected = new PersonWithAddress { Name = "John", Address = new Address { City = "New York" } };
    var actual = new PersonWithAddress { Name = "John", Address = new Address { City = "New York" } };
    Assert.That(actual, Should.BeEquivalentTo(expected));
  }

  [Test]
  public void DifferentTypesSameProperties_ShouldBeEquivalent()
  {
    var expected = new Person { Name = "John", Age = 30 };
    var actual = new Employee { Name = "John", Age = 30 };
    Assert.That(actual, Should.BeEquivalentTo(expected));
  }

  [Test]
  public void SameProperties_ShouldBeEquivalent()
  {
    var expected = new Person { Name = "John", Age = 30 };
    var actual = new Person { Name = "John", Age = 30 };
    Assert.That(actual, Should.BeEquivalentTo(expected));
  }

  [Test]
  public void DifferentProperties_ShouldNotBeEquivalent()
  {
    var expected = new Person { Name = "John", Age = 30 };
    var actual = new Person { Name = "Jane", Age = 30 };
    Assert.Throws<AssertionException>(() =>
    {
      Assert.That(actual, Should.BeEquivalentTo(expected), "Expected failure due to different Name");
    });
  }

  [Test]
  public void Ignore_Test()
  {
    var expected = new Person2
    {
      Name = "John",
      Children = [new Child { ID = 1, Name = "Alice" }]
    };

    var actual = new Person2
    {
      Name = "John",
      Children = [new Child { ID = 2, Name = "Alice" }]
    };

    Assert.That(actual, Should.BeEquivalentTo(expected, options =>
        options
            .ForCollection(a => a.Children)
            .Exclude(b => b.ID)));
  }

  [Test]
  public void Ignore_Order_Test()
  {
    var expected = new Person2
    {
      Children =
        [
          new() { ID = 1, Name = "Alice" },
              new() { ID = 2, Name = "Bob" }
        ]
    };
    var actual = new Person2
    {
      Children =
        [
          new() { ID = 3, Name = "Bob" },
              new() { ID = 4, Name = "Alice" }
        ]
    };

    Assert.That(actual, Should.BeEquivalentTo(expected, options =>
        options.ForCollection(a => a.Children)
            .IgnoreOrder()
            .Exclude(c => c.ID)));
  }


}