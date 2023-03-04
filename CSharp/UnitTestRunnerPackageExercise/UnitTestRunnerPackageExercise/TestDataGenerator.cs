namespace UnitTestRunnerPackageExercise;

public class TestDataGenerator
{
  private readonly Random _random = new();
  private readonly object _lock = new();

  public int GenerateInt()
  {
    lock (_lock)
    {
      return _random.Next();
    }
  }

  public string GenerateString(int length)
  {
    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    lock (_lock)
    {
      return new string(Enumerable.Repeat(chars, length)
        .Select(s => s[_random.Next(s.Length)]).ToArray());
    }
  }
}