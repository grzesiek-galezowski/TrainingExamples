using Microsoft.Playwright;

namespace DemoUiServiceSpecification.Automation;

//bug clean this up
public class HomePageObject(IPage page)
{
  public async Task<string> ReadPageContent()
  {
    return await page.InnerTextAsync("body");
  }

  public async Task WaitForAllSingleScenarioResults()
  {
    await page.WaitForSelectorAsync("text=Result:");
    await page.WaitForSelectorAsync("text=Get Song 2");
  }

  public Task ClickRunSingleScenario()
  {
    return page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Run Single Scenario" }).ClickAsync();
  }

  public async Task EnterSong2Name(string song2Name)
  {
    var song2Input = page.Locator("label:has-text('Song 2 Title') + input");
    await song2Input.FillAsync(song2Name);
  }

  public async Task EnterSong1Name(string song1Name)
  {
    var song1Input = page.Locator("label:has-text('Song 1 Title') + input");
    await song1Input.FillAsync(song1Name);
  }

  public async Task EnterArtistName(string artistName)
  {
    var artistInput = page.Locator("label:has-text('Artist Name') + input");
    await artistInput.FillAsync(artistName);
  }

  public bool IsRequestFailedTextIn(string bodyText)
  {
    return bodyText.Contains("✖ Failed");
  }

  public bool IsRequestSucceededTextIn(string bodyText)
  {
    return bodyText.Contains("✔ Succeeded");
  }

  public async Task NavigateToPageAndWaitToLoad(string url, string selectorToWaitFor)
  {
    await page.GotoAsync(url);
    await page.WaitForSelectorAsync(selectorToWaitFor);
  }

  public async Task WaitForInteractiveUi()
  {
    var firstInput = page.Locator("input.form-input").Nth(0);
    var previousValue = await firstInput.InputValueAsync();
    await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "New Seed" }).ClickAsync();
    await page.WaitForFunctionAsync(
      "(previous) => document.querySelectorAll('input.form-input')[0].value !== previous",
      previousValue);
  }
}