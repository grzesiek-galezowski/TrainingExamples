using static LanguageExt.Prelude;
using UnitTestRunnerPackageExercise;

namespace ExampleTests;

public class LanguageExtCollectionAssertionTests
{
  public void ShouldBeEmpty_ShouldPass_WhenCollectionIsEmpty()
  {
    var emptyCollection = Seq<int>();
    emptyCollection.ShouldBeEmpty();
  }

  public void ShouldBeEmpty_ShouldFail_WhenCollectionIsNotEmpty()
  {
    var collection = Seq(1, 2, 3, 4);
    collection.ShouldNotBeEmpty();
  }

  public void ShouldContainItem_ShouldPass_WhenCollectionContainsItem()
  {
    var collection = Seq(1, 2, 3, 4);
    collection.ShouldContain(2);
  }

  public void ShouldContainItem_ShouldFail_WhenCollectionDoesNotContainItem()
  {
    var collection = Seq(1, 2, 3, 4);
    collection.ShouldNotContain(5);
  }

  public void ShouldContainPredicate_ShouldPass_WhenCollectionContainsMatchingItem()
  {
    var collection = Seq(1, 2, 3, 4);
    collection.ShouldContain(x => x > 3);
  }

  public void ShouldContainPredicate_ShouldFail_WhenCollectionDoesNotContainMatchingItem()
  {
    var collection = Seq(1, 2, 3, 4);
    collection.ShouldNotContain(x => x < 0);
  }

  public void ShouldNotContainItem_ShouldPass_WhenCollectionDoesNotContainItem()
  {
    var collection = Seq(1, 2, 3, 4);
    collection.ShouldNotContain(5);
  }

  public void ShouldNotContainPredicate_ShouldPass_WhenCollectionDoesNotContainMatchingItem()
  {
    var collection = Seq(1, 2, 3, 4);
    collection.ShouldNotContain(x => x < 0);
  }

  public void ShouldBeEquivalentTo_ShouldPass_WhenCollectionsAreEquivalent()
  {
    var collection = Seq(1, 2, 3, 4);
    var equivalentCollection = Seq(4, 3, 2, 1);

    collection.ShouldBeEquivalentTo(equivalentCollection);
  }
}