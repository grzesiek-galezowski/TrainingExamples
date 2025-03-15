using EquivalencyConstraints.EquivalenceAssertions;
using EquivalencyConstraintsSpecification.Fixture;

namespace EquivalencyConstraintsSpecification;

public class SubsetEquivalencySpecification
{
  [Test]
  public void ExtraProperties_ShouldNotBeEquivalent()
  {
    var expected = new Person { Name = "John", Age = 30 };
    var actual = new PersonWithExtra { Name = "John", Age = 30, Extra = "Extra" };
    Assert.That(actual, Should.BeEquivalentTo(expected));
  }

}