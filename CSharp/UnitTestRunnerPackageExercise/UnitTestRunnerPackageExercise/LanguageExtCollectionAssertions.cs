using LanguageExt;

namespace UnitTestRunnerPackageExercise;

public static class LanguageExtCollectionAssertions
{
  public static void ShouldBeEmpty<T>(this Seq<T> collection)
  {
    Assert.IsTrue(collection.IsEmpty, "Expected collection to be empty");
  }

  public static void ShouldNotBeEmpty<T>(this Seq<T> collection)
  {
    Assert.IsTrue(!collection.IsEmpty, "Expected collection to not be empty");
  }

  public static void ShouldContain<T>(this Seq<T> collection, T item)
  {
    Assert.IsTrue(collection.Contains(item), $"Expected collection to contain {item}");
  }

  public static void ShouldContain<T>(this Seq<T> collection, Func<T, bool> predicate)
  {
    Assert.IsTrue(collection.Exists(predicate), $"Expected collection to contain an item that matches the predicate");
  }

  public static void ShouldNotContain<T>(this Seq<T> collection, T item)
  {
    Assert.IsFalse(collection.Contains(item), $"Expected collection not to contain {item}");
  }

  public static void ShouldNotContain<T>(this Seq<T> collection, Func<T, bool> predicate)
  {
    Assert.IsFalse(collection.Exists(predicate), $"Expected collection not to contain an item that matches the predicate");
  }

  public static void ShouldBeEquivalentTo<T>(this Seq<T> collection, IEnumerable<T> expected)
  {
    Assert.IsTrue(collection.ToHashSet().SetEquals(expected), $"Expected collection to be equivalent to {expected}");
  }
}