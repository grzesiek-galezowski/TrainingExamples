namespace UnitTestRunnerPackageExercise;

public static class Assert
{
  public static void AreEqual(int expected, int actual)
  {
    if (expected != actual)
    {
      throw new AssertionException(nameof(AreEqual), $"{actual} is not equal to expected {expected}");
    }
  }

  public static void Fail(string message)
  {
    throw new AssertionException(nameof(Fail), message);
  }
}

//bug JsonAssert etc.
//bug Lang Ext assert
//bug random generator