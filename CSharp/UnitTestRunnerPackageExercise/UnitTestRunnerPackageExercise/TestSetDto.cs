using System.Collections.Immutable;

namespace UnitTestRunnerPackageExercise;

public record TestSetDto(ImmutableArray<TestSuiteDto> Suites);