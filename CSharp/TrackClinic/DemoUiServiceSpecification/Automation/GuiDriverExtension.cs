using Flurl.Http;
using Microsoft.Playwright;
using ServiceSpecificationBuildingBlocks.Browser;

namespace DemoUiServiceSpecification.Automation;

//bug understand this class

public class GuiDriverExtension(FlurlClient client, CancellationToken cancellationToken) //bug
{
  private const string ExpectedHeader = "TrackClinic Demo & Traffic Generator";

  public async Task<SingleScenarioUiResponse> RunSingleScenario(string artistName, string song1Name, string song2Name)
  {
    using var playwright = await Playwright.CreateAsync();
    await using var browser = await playwright.Chromium.ConnectAsync(SharedPlaywrightInstance.GetConnectionString());
    var page = await browser.NewPageAsync();

    var homePageObject = new HomePageObject(page);
    await homePageObject.NavigateToPageAndWaitToLoad(PageAddress(), $"text={ExpectedHeader}");
    await homePageObject.WaitForInteractiveUi();

    await homePageObject.EnterArtistName(artistName);
    await homePageObject.EnterSong1Name(song1Name);
    await homePageObject.EnterSong2Name(song2Name);

    await homePageObject.ClickRunSingleScenario();

    await homePageObject.WaitForAllSingleScenarioResults();

    var bodyText = await homePageObject.ReadPageContent();

    return new SingleScenarioUiResponse(
      homePageObject.IsRequestSucceededTextIn(bodyText),
      homePageObject.IsRequestFailedTextIn(bodyText),
      bodyText);
  }

  private string PageAddress()
  {
    return new Uri(BrowserReachableBaseAddress(GetAppBaseAddress()), "/").ToString();
  }

  public async Task<string> GetHomePageBodyText()
  {
    using var playwright = await Playwright.CreateAsync();
    await using var browser = await playwright.Chromium.ConnectAsync(SharedPlaywrightInstance.GetConnectionString());
    var page = await browser.NewPageAsync();

    var browserBaseAddress = BrowserReachableBaseAddress(GetAppBaseAddress());
    await new HomePageObject(page).NavigateToPageAndWaitToLoad(new Uri(browserBaseAddress, "/").ToString(), "text=TrackClinic Demo & Traffic Generator");

    return await new HomePageObject(page).ReadPageContent();
  }

  private static Uri BrowserReachableBaseAddress(Uri appBaseAddress)
  {
    var builder = new UriBuilder(appBaseAddress)
    {
      Host = "host.docker.internal"
    };
    return builder.Uri;
  }

  private Uri GetAppBaseAddress()
  {
    return new Uri(client.BaseUrl);
  }

}