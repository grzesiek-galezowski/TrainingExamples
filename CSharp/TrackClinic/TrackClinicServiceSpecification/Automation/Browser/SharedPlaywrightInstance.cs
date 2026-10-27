using Testcontainers.Playwright;

namespace TrackClinicServiceSpecification.Automation.Browser;

public static class SharedPlaywrightInstance
{
  private static readonly PlaywrightContainer PlaywrightContainer =
    new PlaywrightBuilder("mcr.microsoft.com/playwright:v1.55.1").Build();

  public static async Task Start(CancellationToken cancellationToken)
  {
    await PlaywrightContainer.StartAsync(cancellationToken);
  }

  public static async Task Stop(CancellationToken cancellationToken)
  {
    await PlaywrightContainer.StopAsync(cancellationToken);
    await PlaywrightContainer.DisposeAsync();
  }

  public static string GetConnectionString() => PlaywrightContainer.GetConnectionString();
}