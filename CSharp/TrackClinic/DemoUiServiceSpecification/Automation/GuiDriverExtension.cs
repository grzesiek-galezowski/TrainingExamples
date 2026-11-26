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

    await NavigateToPageAndWaitToLoad(page, PageAddress(), $"text={ExpectedHeader}");
    await WaitForInteractiveUi(page);

    await EnterArtistName(artistName, page);
    await EnterSong1Name(song1Name, page);
    await EnterSong2Name(song2Name, page);

    await ClickRunSingleScenario(page);

    await WaitForAllSingleScenarioResults(page);

    var bodyText = await ReadPageContent(page);

    return new SingleScenarioUiResponse(
      IsRequestSucceededTextIn(bodyText),
      IsRequestFailedTextIn(bodyText),
      bodyText);
  }

  private string PageAddress()
  {
    return new Uri(BrowserReachableBaseAddress(GetAppBaseAddress()), "/").ToString();
  }

  private static async Task<string> ReadPageContent(IPage page)
  {
    return await page.InnerTextAsync("body");
  }

  private static async Task WaitForAllSingleScenarioResults(IPage page)
  {
    await page.WaitForSelectorAsync("text=Result:");
    await page.WaitForSelectorAsync("text=Get Song 2");
  }

  private static Task ClickRunSingleScenario(IPage page)
  {
    return page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Run Single Scenario" }).ClickAsync();
  }

  private static async Task EnterSong2Name(string song2Name, IPage page)
  {
    var song2Input = page.Locator("label:has-text('Song 2 Title') + input");
    await song2Input.FillAsync(song2Name);
  }

  private static async Task EnterSong1Name(string song1Name, IPage page)
  {
    var song1Input = page.Locator("label:has-text('Song 1 Title') + input");
    await song1Input.FillAsync(song1Name);
  }

  private static async Task EnterArtistName(string artistName, IPage page)
  {
    var artistInput = page.Locator("label:has-text('Artist Name') + input");
    await artistInput.FillAsync(artistName);
  }

  private static bool IsRequestFailedTextIn(string bodyText)
  {
    return bodyText.Contains("✖ Failed");
  }

  private static bool IsRequestSucceededTextIn(string bodyText)
  {
    return bodyText.Contains("✔ Succeeded");
  }

  public async Task<string> GetHomePageBodyText()
  {
    using var playwright = await Playwright.CreateAsync();
    await using var browser = await playwright.Chromium.ConnectAsync(SharedPlaywrightInstance.GetConnectionString());
    var page = await browser.NewPageAsync();

    var browserBaseAddress = BrowserReachableBaseAddress(GetAppBaseAddress());
    await NavigateToPageAndWaitToLoad(page, new Uri(browserBaseAddress, "/").ToString(), "text=TrackClinic Demo & Traffic Generator");

    return await ReadPageContent(page);
  }

  private static async Task NavigateToPageAndWaitToLoad(IPage page, string url, string selectorToWaitFor)
  {
    await page.GotoAsync(url);
    await page.WaitForSelectorAsync(selectorToWaitFor);
  }

  private static async Task WaitForInteractiveUi(IPage page)
  {
    var firstInput = page.Locator("input.form-input").Nth(0);
    var previousValue = await firstInput.InputValueAsync();
    await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "New Seed" }).ClickAsync();
    await page.WaitForFunctionAsync(
      "(previous) => document.querySelectorAll('input.form-input')[0].value !== previous",
      previousValue);
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