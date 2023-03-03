using System.Collections.Immutable;

namespace UnitTestRunnerPackageExercise;

public record TestSuiteDto(string SuiteName, ImmutableArray<TestReportDto> Tests);