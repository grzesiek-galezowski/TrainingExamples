namespace ServiceSpecificationBuildingBlocks.Browser;

public class GuiScenarioRunner(Uri appBaseAddress)
{
  public Task<string> RunSingleScenario(string artistName, string song1Name, string song2Name)
  {
    return AtHomePage(async homePage =>
    {
      await homePage.EnterArtistName(artistName);
      await homePage.EnterSong1Name(song1Name);
      await homePage.EnterSong2Name(song2Name);
      await homePage.ClickRunSingleScenario();
      await homePage.WaitForAllSingleScenarioResults();
      return await homePage.ReadPageContent();
    });
  }

  public Task<string> GetHomePageBodyText()
  {
    return AtHomePage(homePage => homePage.ReadPageContent());
  }

  private async Task<string> AtHomePage(Func<HomePageObject, Task<string>> action)
  {
    await using var browserEngine = await BrowserEngine.CreateAsync();
    var homePage = await HomePageObject.Create(browserEngine.Browser, PageAddress());
    await homePage.Load();
    return await action(homePage);
  }

  private string PageAddress() => new Uri(BrowserReachableBaseAddress(appBaseAddress), "/").ToString();

  private static Uri BrowserReachableBaseAddress(Uri appBaseAddress)
  {
    return new UriBuilder(appBaseAddress)
    {
      Host = "host.docker.internal"
    }.Uri;
  }
}