namespace UnitTestRunnerPackageExercise;

public record FullyQualifiedTestName(string Namespace, string SuiteName, string Name)
{
  private string Namespace { get; init; } = Namespace;
  private string SuiteName { get; init; } = SuiteName;
  private string Name { get; init; } = Name;

  public override string ToString()
  {
    return $"{Namespace}.{SuiteName}.{Name}";
  }
}