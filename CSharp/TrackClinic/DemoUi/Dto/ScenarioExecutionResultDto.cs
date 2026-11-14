using DemoUi.Logic;

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
)
{
  public static ScenarioExecutionResultDto Error(SeededScenarioPreset preset, Exception ex)
  {
    return new ScenarioExecutionResultDto(
      ScenarioId: "error",
      ArtistName: preset.Artist,
      Song1Title: preset.Song1,
      Song2Title: preset.Song2,
      Success: false,
      TotalElapsedMilliseconds: 0,
      Timestamp: DateTime.UtcNow,
      Steps:
      [
        ScenarioStepResult.Error(ex)
      ]);
  }
}