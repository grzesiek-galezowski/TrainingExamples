using Core.Maybe;

namespace UnitTestRunnerPackageExercise;

public record TestReportDto
{
  public FullyQualifiedTestName FullyQualifiedTestName { get; }
  public TestStatus Status { get; init; }= TestStatus.Started;
  public Maybe<Exception> Exception { get; init; }

  public TestReportDto(FullyQualifiedTestName fullyQualifiedTestName)
  {
    FullyQualifiedTestName = fullyQualifiedTestName;
  }
}