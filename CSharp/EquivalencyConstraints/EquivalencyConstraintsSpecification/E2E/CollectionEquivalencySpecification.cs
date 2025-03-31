using System.Collections.Immutable;
using EquivalencyConstraints.EquivalenceAssertions;
using EquivalencyConstraintsSpecification.E2E.Fixture;

namespace EquivalencyConstraintsSpecification.E2E;

public class CollectionEquivalencySpecification
{
  [Test]
  public void ShouldPassForDifferentCollectionsWithSameItems()
  {
    var expected = new List<int> { 1, 2, 3 };
    var actual = new[] { 1, 2, 3 };
    Assert.That(actual, Should.BeEquivalentTo(expected));
  }

  [Test]
  public void ShouldFailForSameCollectionTypeWithDifferentItems()
  {
    var expected = new List<int> { 1, 2, 3 };
    var actual = new List<int> { 1, 2, 4 };

    Assertions.AssertEquivalencyAssertionFailedWhenComparing(
      expected,
      actual,
      "< 1, 2, 3 >",
      "< 1, 2, 4 >");
  }

  [Test]
  public void ShouldFailWhenCollectionInCollectionHasWrongItems()
  {
    var expected = new List<List<int>> { new() { 1, 2, 3 } };
    var actual = new List<List<int>> { new() { 1, 2, 4 } };

    Assertions.AssertEquivalencyAssertionFailedWhenComparing(
      expected,
      actual,
      "< < 1, 2, 3 > >",
      "< < 1, 2, 4 > >");
  }

  [Test]
  public void ShouldPassWhenCollectionInCollectionHasWrongOrderOfItemsButIgnoreOrderIsEnabled()
  {
    var expected = new List<List<int>> { new() { 1, 2, 3 } };
    var actual = new List<List<int>> { new() { 1, 2, 3 } };

    Assert.That(actual,
      Should.BeEquivalentTo(expected, options => options.ForCollection(o => o[0]).IgnoreOrder()));
    Assert.That(actual,
      Should.BeEquivalentTo(expected, options => options.ForCollection(o => o.First()).IgnoreOrder()));
  }

  [Test]
  public void ShouldFailWhenOnlyOneCollectionInCollectionHasIgnoreOrderAndAnotherHasWrongOrder()
  {
    var expected = new List<List<int>>
    {
      new() { 1, 2, 3 },
      new() { 1, 2, 3 }
    };
    var actual = new List<List<int>>
    {
      new() { 1, 2, 3 },
      new() { 1, 3, 2 },
    };

    Assert.That(actual,
      Should.BeEquivalentTo(expected, options => options.ForCollection(o => o[1]).IgnoreOrder()));
  }

  [Test]
  public void ShouldPassForEquivalentObjectsWithCollections()
  {
    var expected = new ClassWithCollection { Numbers = [1, 2, 3] };
    var actual = new ClassWithCollection { Numbers = [1, 2, 3] };
    Assert.That(actual, Should.BeEquivalentTo(expected));
  }

  [Test]
  public void ShouldPassForObjectsWithEquivalentCollectionsOfDifferentTypes()
  {
    var expected = new ClassWithCollection { Numbers = new List<int> {1, 2, 3} };
    var actual = new ClassWithCollection { Numbers = ImmutableList.Create(new[] { 1, 2, 3 }) };
    Assert.That(actual, Should.BeEquivalentTo(expected));
  }

  [Test]
  public void ShouldPassForObjectsWithCollectionsOfDifferentOrderWhenOrderIsIgnored()
  {
    var expected = new ClassWithCollection { Numbers = new List<int> {1, 2, 3} };
    var actual = new ClassWithCollection { Numbers = ImmutableList.Create(new[] { 2, 1, 3 }) };
    Assert.That(actual,
      Should.BeEquivalentTo(expected, options => options.ForCollection(o => o.Numbers).IgnoreOrder()));
  }

  [Test]
  public void ShouldFailForObjectsWithNonEquivalentCollectionsOfDifferentTypes()
  {
    var expected = new ClassWithCollection { Numbers = [1, 2, 3] };
    var actual = new ClassWithCollection { Numbers = [1, 1, 3] };
    Assertions.AssertEquivalencyAssertionFailedWhenComparing(expected,
      actual,
      "{ Numbers: [ 1, 2, 3 ] }",
      "{ Numbers: [ 1, 1, 3 ] }");
  }
}