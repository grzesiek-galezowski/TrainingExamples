using AwesomeAssertions;
using Flurl.Http;
using Microsoft.Playwright;
using TrackClinicServiceSpecification.Automation.Keycloak;

namespace TrackClinicServiceSpecification.Automation;

public class OperationsDriverExtension(Lazy<FlurlClient> client, string keycloakRealmName, CancellationToken cancellationToken)
{
  public async Task<OpenApiDocumentResponse> GetOpenApiDocument()
  {
    var response = await new TrackClinicRequestBuilder(
        client.Value.Request("/openapi/v1.json"),
        await SharedKeycloakInstance.GetTestAuthTokens(keycloakRealmName))
      .Build()
      .GetAsync(cancellationToken: cancellationToken);
    response.StatusCode.Should().Be(200);
    return new OpenApiDocumentResponse(await response.GetStringAsync());
  }

  public async Task<MetricsResponse> GetMetrics()
  {
    var response = await client.Value.Request("/metrics")
      .GetAsync(cancellationToken: cancellationToken);
    return new MetricsResponse(response);
  }

  public CapturedActivities StartActivityTracking()
  {
    return new CapturedActivities();
  }

  public async Task<WebPageResponse> GetScalarUi()
  {
    using var playwright = await Playwright.CreateAsync();
    await using var browser = await playwright.Chromium.LaunchAsync(
      new BrowserTypeLaunchOptions
      {
        Headless = true
      });
    var page = await browser.NewPageAsync();

    await SetupProxyingRequestsToTestServer(page, "http://localhost/**/*", cancellationToken);
    await NavigateToPageAndWaitToLoad(page, "http://localhost/scalar/v1", "text=TrackClinic API");

    var title = await page.TitleAsync();
    var bodyText = await page.InnerTextAsync("body");
    return new WebPageResponse(title, bodyText);
  }

  private static async Task NavigateToPageAndWaitToLoad(IPage page, string url, string selectorToWaitFor)
  {
    await page.GotoAsync(url);
    await page.WaitForSelectorAsync(selectorToWaitFor);
  }

  private async Task SetupProxyingRequestsToTestServer(IPage page, string url, CancellationToken cancellationToken)
  {
    await page.RouteAsync(
      url,
      async route =>
      {
        var response = await RerouteToTestServerClient(route.Request, cancellationToken);

        await route.FulfillAsync(
          new RouteFulfillOptions
          {
            BodyBytes = await response.GetBytesAsync(),
            Status = response.StatusCode,
            Headers = response.Headers.ToDictionary()
          });
      });
  }

  private async Task<IFlurlResponse> RerouteToTestServerClient(IRequest routeRequest, CancellationToken ct)
  {
    return await client.Value
      .Request(new Uri(routeRequest.Url).PathAndQuery)
      .AllowAnyHttpStatus()
      .SendAsync(new HttpMethod(routeRequest.Method), cancellationToken: ct);
  }
}