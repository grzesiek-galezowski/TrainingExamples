using ServiceSpecificationBuildingBlocks.Browser;

namespace TrackClinic.AppHostSpecification.Automation;

public class GuiDriverExtension(Uri appBaseAddress)
{
  private readonly GuiScenarioRunner _scenarioRunner = new(appBaseAddress);

  public async Task<SingleScenarioUiResponse> RunSingleScenario(string artistName, string song1Name, string song2Name)
  {
    return new SingleScenarioUiResponse(await _scenarioRunner.RunSingleScenario(artistName, song1Name, song2Name));
  }
}
