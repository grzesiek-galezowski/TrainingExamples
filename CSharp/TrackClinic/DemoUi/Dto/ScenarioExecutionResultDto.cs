namespace DemoUi.Dto;

public record ScenarioExecutionResultDto(
  string ScenarioId,
  string ArtistName,
  string Song1Title,
  string Song2Title,
  bool Success,
  long TotalElapsedMilliseconds,
  DateTime Timestamp,
  IReadOnlyList<ScenarioStepResult> Steps
);