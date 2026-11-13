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
  public static ScenarioExecutionResultDto Error(string selectedSong1, string selectedSong2, string selectedArtist,
    Exception ex)
  {
    return new ScenarioExecutionResultDto(
      ScenarioId: "error",
      ArtistName: selectedArtist,
      Song1Title: selectedSong1,
      Song2Title: selectedSong2,
      Success: false,
      TotalElapsedMilliseconds: 0,
      Timestamp: DateTime.UtcNow,
      Steps:
      [
        ScenarioStepResult.Error(ex)
      ]);
  }
}