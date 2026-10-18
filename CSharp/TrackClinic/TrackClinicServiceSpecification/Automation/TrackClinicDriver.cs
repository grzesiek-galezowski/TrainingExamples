﻿using AwesomeAssertions;
 using AwesomeAssertions.Extensions;
 using Flurl.Http;
 using Microsoft.Playwright;
 using TddXt.AnyRoot;
using TddXt.AnyRoot.Strings;
using TrackClinic;

namespace TrackClinicServiceSpecification.Automation;

public class TrackClinicDriver : IAsyncDisposable
{
  private readonly string _databaseName;
  private readonly CancellationToken _cancellationToken;
  private readonly WebApp _webApp;
  private readonly Lazy<FlurlClient> _client;

  public TrackClinicDriver(CancellationToken cancellationToken)
  {
    _databaseName = Any.String("testDb");
    _cancellationToken = cancellationToken;
    _webApp = new WebApp(_databaseName);
    _client = new Lazy<FlurlClient>(() => new FlurlClient(_webApp.CreateClient()));
  }

  public async ValueTask DisposeAsync()
  {
    await _webApp.DisposeAsync();
    await SharedMongoInstance.DropDatabase(_databaseName, _cancellationToken);
  }

  public async Task Start()
  {
    _webApp.StartServer();
    await WaitForHealthCheck("/health", 10.Seconds());
    await WaitForHealthCheck("/ready", 10.Seconds());
  }

  public async Task<HealthResponse> RequestHealthCheck(string urlSegment)
  {
    return new HealthResponse(await _client.Value.Request(urlSegment)
      .AllowAnyHttpStatus()
      .GetAsync(cancellationToken: _cancellationToken));
  }

  public async Task CreateSongProject(string artistName, CreateSongRequestDto createSongRequestDto)
  {
    var response = await _client.Value.Request($"/api/v1/artists/{artistName}/songs")
      .PostJsonAsync(createSongRequestDto, cancellationToken: _cancellationToken);
    response.StatusCode.Should().Be(201);
  }

  public async Task<RetrieveProjectDetailsResponse> RetrieveProjectDetails(string artist, string songName)
  {
    var response = await _client.Value.Request($"/api/v1/artists/{artist}/songs/{songName}")
      .GetAsync(cancellationToken: _cancellationToken);
    response.StatusCode.Should().Be(200);
    return new RetrieveProjectDetailsResponse(await response.GetJsonAsync<SongDetailsDto>());
  }

  public async Task<OpenApiDocumentResponse> GetOpenApiDocument()
  {
    var response = await _client.Value.Request("/openapi/v1.json")
      .GetAsync(cancellationToken: _cancellationToken);
    response.StatusCode.Should().Be(200);
    return new OpenApiDocumentResponse(await response.GetStringAsync());
  }

  public async Task<WebPageResponse> GetScalarUi()
  {
    using var playwright = await Playwright.CreateAsync();
    await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
    {
      Headless = true
    });
    var page = await browser.NewPageAsync();

    await SetupProxyingRequestsToTestServer(page, "http://localhost/**/*", _cancellationToken);
    await NavigateToPageAndWaitToLoad(page, "http://localhost/scalar/v1", "text=TrackClinic API");

    return new WebPageResponse(page);
  }

  private async Task WaitForHealthCheck(string urlSegment, TimeSpan timeout)
  {
    using var cts = new CancellationTokenSource(timeout);
    using var timer = new PeriodicTimer(TimeSpan.FromMilliseconds(100));
    while (await timer.WaitForNextTickAsync(cts.Token))
    {
      if (await (await RequestHealthCheck(urlSegment)).IsHealthy())
      {
        return;
      }
    }
  }

  private static async Task NavigateToPageAndWaitToLoad(IPage page, string url, string selectorToWaitFor)
  {
    await page.GotoAsync(url);
    await page.WaitForSelectorAsync(selectorToWaitFor);
  }

  private async Task SetupProxyingRequestsToTestServer(IPage page, string url, CancellationToken cancellationToken)
  {
    await page.RouteAsync(url, async route =>
    {
      var response = await RerouteToTestServerClient(route.Request, cancellationToken);

      await route.FulfillAsync(new RouteFulfillOptions
      {
        BodyBytes = await response.GetBytesAsync(),
        Status = response.StatusCode,
        Headers = response.Headers.ToDictionary()
      });
    });
  }

  private async Task<IFlurlResponse> RerouteToTestServerClient(IRequest routeRequest, CancellationToken ct)
  {
    return await _client.Value
      .Request(new Uri(routeRequest.Url).PathAndQuery)
      .AllowAnyHttpStatus()
      .SendAsync(new HttpMethod(routeRequest.Method), cancellationToken: ct);
  }
}