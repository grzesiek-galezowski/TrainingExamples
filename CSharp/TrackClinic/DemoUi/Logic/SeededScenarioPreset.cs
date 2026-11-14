namespace DemoUi.Logic;

public record SeededScenarioPreset(string Artist, string Song1, string Song2)
{
  public static SeededScenarioPreset CreateRandomInstanceWithConsistentId()
  {
    var id = Random.Shared.Next(1000, 9999);

    return new SeededScenarioPreset(
      $"Artist{Guid.NewGuid()}_{id}",
      $"Song1{Guid.NewGuid()}_{id}",
      $"Song2{Guid.NewGuid()}_{id}");
  }
}