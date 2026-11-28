using Flurl.Http;
using ServiceSpecificationBuildingBlocks.Browser;

namespace DemoUiServiceSpecification.Automation;

public class GuiDriverExtension(FlurlClient client)
{
  private readonly GuiScenarioRunner _scenarioRunner = new(new Uri(client.BaseUrl));

  public async Task<SingleScenarioUiTestResponse> RunSingleScenario(string artistName, string song1Name, string song2Name)
  {
    return new SingleScenarioUiTestResponse(await _scenarioRunner.RunSingleScenario(artistName, song1Name, song2Name));
  }

  public async Task<string> GetHomePageBodyText()
  {
    return await _scenarioRunner.GetHomePageBodyText();
  }
}