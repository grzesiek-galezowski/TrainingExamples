using Flurl.Http;
using ServiceSpecificationBuildingBlocks.Browser;

namespace DemoUiServiceSpecification.Automation;

//bug understand this class

public class GuiDriverExtension(FlurlClient client, CancellationToken cancellationToken) //bug
{
  public async Task<SingleScenarioUiTestResponse> RunSingleScenario(string artistName, string song1Name, string song2Name)
  {
    await using var browserEngine = await BrowserEngine.CreateAsync();

    var homePage = await HomePageObject.Create(browserEngine.Browser, PageAddress());
    await homePage.Load();

    await homePage.EnterArtistName(artistName);
    await homePage.EnterSong1Name(song1Name);
    await homePage.EnterSong2Name(song2Name);

    await homePage.ClickRunSingleScenario();

    await homePage.WaitForAllSingleScenarioResults();

    return new SingleScenarioUiTestResponse(await homePage.ReadPageContent());
  }

  public async Task<string> GetHomePageBodyText()
  {
    // bug duplication of code from RunSingleScenario, refactor to avoid duplication
    await using var browserEngine = await BrowserEngine.CreateAsync();

    var homePageObject = await HomePageObject.Create(browserEngine.Browser, PageAddress());
    await homePageObject.Load();

    return await homePageObject.ReadPageContent();
  }

  private Uri GetAppBaseAddress() => new(client.BaseUrl);

  private string PageAddress() => new Uri(BrowserReachableBaseAddress(GetAppBaseAddress()), "/").ToString();

  private static Uri BrowserReachableBaseAddress(Uri appBaseAddress)
  {
    var builder = new UriBuilder(appBaseAddress)
    {
      Host = "host.docker.internal"
    };
    return builder.Uri;
  }

}