namespace UnitTestRunnerPackageExercise;

public static class Assert
{
  public static void AreEqual<T>(T expected, T actual)
  {
    NotNull(expected);
    if (!expected.Equals(actual))
    {
      throw new AssertionException(nameof(AreEqual), $"{actual} is not equal to expected {expected}");
    }
  }

  private static void NotNull<T>(T expected)
  {
    if (expected == null)
    {
      throw new AssertionException(nameof(NotNull), "Should be not null, but is null");
    }
  }

  public static void Fail(string message)
  {
    throw new AssertionException(nameof(Fail), message);
  }

  public static void IsTrue(bool value)
  {
    if (!value) throw new AssertionException(nameof(IsTrue), "is false");
  }

  public static void IsTrue(bool value, string message)
  {
    if (!value) throw new AssertionException(nameof(IsTrue), message);
  }

  public static void IsFalse(bool value, string message)
  {
    if (value) throw new AssertionException(nameof(IsFalse), message);
  }

  public static void AreEquivalent<T>(IEnumerable<T> expected, IEnumerable<T> actual, string message = null)
  {
    if (expected == null)
    {
      throw new ArgumentNullException(nameof(expected));
    }

    if (actual == null)
    {
      throw new ArgumentNullException(nameof(actual));
    }

    if (ReferenceEquals(expected, actual))
    {
      return;
    }

    var expectedList = expected.ToList();
    var actualList = actual.ToList();

    if (expectedList.Count != actualList.Count || !expectedList.Intersect(actualList).Count().Equals(expectedList.Count))
    {
      Fail(message ?? "The collections are not equivalent.");
    }
  }

}
