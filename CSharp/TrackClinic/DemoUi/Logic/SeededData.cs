namespace DemoUi.Logic;

public static class SeededData
{
  public static SeededScenarioPreset GetRandomPreset()
  {
    return new SeededScenarioPreset("Artist" + Guid.NewGuid(), "Song1" + Guid.NewGuid(), "Song2" + Guid.NewGuid());
  }
}