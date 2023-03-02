namespace UnitTestRunnerPackageExercise;

public class AssertionException : Exception
{
  public AssertionException(string assertionName, string message)
    : base($"{assertionName}: {message}")
  {
    
  }
}