using System.Collections.Immutable;
using System.Diagnostics;
using AwesomeAssertions;
using Flurl.Http;
using Microsoft.Playwright;
using TrackClinicServiceSpecification.Automation.Keycloak;
using TrackClinicServiceSpecification.Automation.Browser;
using TrackClinicServiceSpecification.Automation.Zipkin;

namespace TrackClinicServiceSpecification.Automation;

public class OperationsDriverExtension(Lazy<FlurlClient> client, string keycloakRealmName, CancellationToken cancellationToken, IZipkinClient zipkinClient)
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

  public async Task ShouldHaveCapturedTrace(ActivityTraceId traceId, string expectedMethod, int expectedStatusCode)
  {
    var timeout = TimeSpan.FromSeconds(5);
    var spans = await WaitForTrace(traceId, timeout);

    spans.Should().NotBeEmpty(
      $"Trace with ID {traceId} should have been captured in Zipkin within {timeout}.");

    spans.Should().Contain(span =>
      span.Tags != null &&
      span.Tags.ContainsKey("http.request.method") && span.Tags["http.request.method"] == expectedMethod &&
      span.Tags.ContainsKey("http.response.status_code") && span.Tags["http.response.status_code"] == expectedStatusCode.ToString());
  }

  private async Task<ImmutableList<ZipkinSpan>> WaitForTrace(ActivityTraceId traceId, TimeSpan timeout)
  {
    using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
    cts.CancelAfter(timeout);
    using var timer = new PeriodicTimer(TimeSpan.FromMilliseconds(100));

    try
    {
      do
      {
        var spans = await zipkinClient.GetTrace(traceId.ToHexString(), cts.Token);
        if (spans.Count > 0)
        {
          return spans;
        }
      } while (await timer.WaitForNextTickAsync(cts.Token));
    }
    catch (Exception) when (cts.IsCancellationRequested)
    {
    }

    return [];
  }

  public async Task<WebPageResponse> GetScalarUi()
  {
    using var playwright = await Playwright.CreateAsync();
    await using var browser = await playwright.Chromium.ConnectAsync(SharedPlaywrightInstance.GetConnectionString());
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