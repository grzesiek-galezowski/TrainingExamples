using Microsoft.Playwright;
using ServiceSpecificationBuildingBlocks.Browser;

namespace DemoUiServiceSpecification.Automation;

//bug understand this class
public class GuiDriverExtension(Func<Uri> appBaseAddress, CancellationToken cancellationToken) //bug
{
  public async Task<SingleScenarioUiResponse> RunSingleScenario(string artistName, string song1Name, string song2Name)
  {
    using var playwright = await Playwright.CreateAsync();
    await using var browser = await playwright.Chromium.ConnectAsync(SharedPlaywrightInstance.GetConnectionString());
    var page = await browser.NewPageAsync();

    var browserBaseAddress = BrowserReachableBaseAddress(appBaseAddress());
    await NavigateToPageAndWaitToLoad(page, new Uri(browserBaseAddress, "/").ToString(), "text=TrackClinic Demo & Traffic Generator");
    await WaitForInteractiveUi(page);

    var artistInput = page.Locator("label:has-text('Artist Name') + input");
    var song1Input = page.Locator("label:has-text('Song 1 Title') + input");
    var song2Input = page.Locator("label:has-text('Song 2 Title') + input");

    await artistInput.FillAsync(artistName);
    await song1Input.FillAsync(song1Name);
    await song2Input.FillAsync(song2Name);

    await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Run Single Scenario" }).ClickAsync();

    await page.WaitForSelectorAsync("text=Result:");
    await page.WaitForSelectorAsync("text=Get Song 2");

    var bodyText = await page.InnerTextAsync("body");

    return new SingleScenarioUiResponse(
      bodyText.Contains("✔ Succeeded"),
      bodyText.Contains("✖ Failed"),
      bodyText);
  }

  public async Task<string> GetHomePageBodyText()
  {
    using var playwright = await Playwright.CreateAsync();
    await using var browser = await playwright.Chromium.ConnectAsync(SharedPlaywrightInstance.GetConnectionString());
    var page = await browser.NewPageAsync();

    var browserBaseAddress = BrowserReachableBaseAddress(appBaseAddress());
    await NavigateToPageAndWaitToLoad(page, new Uri(browserBaseAddress, "/").ToString(), "text=TrackClinic Demo & Traffic Generator");

    return await page.InnerTextAsync("body");
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
    var builder = new UriBuilder(appBaseAddress);
    if (builder.Host is "localhost" or "127.0.0.1")
    {
      builder.Host = "host.docker.internal";
    }

    return builder.Uri;
  }
}