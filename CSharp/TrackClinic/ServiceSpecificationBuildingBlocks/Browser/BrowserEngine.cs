using Microsoft.Playwright;

namespace ServiceSpecificationBuildingBlocks.Browser;

public sealed class BrowserEngine : IAsyncDisposable
{
  private readonly IPlaywright _playwright;

  private BrowserEngine(IPlaywright playwright, IBrowser browser)
  {
    _playwright = playwright;
    Browser = browser;
  }

  public IBrowser Browser { get; }

  public static async Task<BrowserEngine> CreateAsync()
  {
    var playwright = await Playwright.CreateAsync();
    try
    {
      var browser = await playwright.Chromium.ConnectAsync(SharedPlaywrightInstance.GetConnectionString());
      return new BrowserEngine(playwright, browser);
    }
    catch
    {
      playwright.Dispose();
      throw;
    }
  }

  public async ValueTask DisposeAsync()
  {
    try
    {
      await Browser.DisposeAsync();
    }
    finally
    {
      _playwright.Dispose();
    }
  }
}