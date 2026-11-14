namespace DemoUi.Logic;

public record SeededScenarioPresetDto
{
  public SeededScenarioPresetDto(string Artist, string Song1, string Song2)
  {
    this.Artist = Artist;
    this.Song1 = Song1;
    this.Song2 = Song2;
  }

  public SeededScenarioPresetDto(SeededScenarioPreset preset)
  : this(preset.Artist, preset.Song1, preset.Song2)
  {
  }

  public string Artist { get; set; }
  public string Song1 { get; set; }
  public string Song2 { get; set; }

  public SeededScenarioPreset ToSeededScenarioPreset()
  {
    return new SeededScenarioPreset(Artist, Song1, Song2);
  }
}