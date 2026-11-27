namespace ServiceSpecificationBuildingBlocks.Browser;

public class GuiScenarioRunner(Uri appBaseAddress)
{
  public Task<string> RunSingleScenario(string artistName, string song1Name, string song2Name)
  {
    return WithHomePage(async homePage =>
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
    return WithHomePage(homePage => homePage.ReadPageContent());
  }

  private async Task<string> WithHomePage(Func<HomePageObject, Task<string>> action)
  {
    await using var browserEngine = await BrowserEngine.CreateAsync();
    var homePage = await HomePageObject.Create(browserEngine.Browser, PageAddress());
    await homePage.Load();
    return await action(homePage);
  }

  private string PageAddress() => new Uri(BrowserReachableBaseAddress(appBaseAddress), "/").ToString();

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