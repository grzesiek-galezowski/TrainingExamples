using AwesomeAssertions;
using AwesomeAssertions.Extensions;
using Flurl.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Playwright;
using TddXt.AnyRoot;
using TddXt.AnyRoot.Strings;
using TrackClinic;
using TrackClinicServiceSpecification.Automation.Keycloak;

namespace TrackClinicServiceSpecification.Automation;

public class TrackClinicDriver : IAsyncDisposable
{
  private const string AdminUser = "admin-user";
  private const string UnprivilegedUser = "unprivileged-user";
  private const string ArtistUser = "artist-user";
  private readonly string _databaseName;
  private readonly CancellationToken _cancellationToken;
  private readonly TestAuthTokens _testAuthTokens;
  private readonly WebApp _webApp;
  private readonly Lazy<FlurlClient> _client;

  private TrackClinicDriver(CancellationToken cancellationToken, TestAuthTokens testAuthTokens)
  {
    _databaseName = Any.String("testDb");
    _cancellationToken = cancellationToken;
    _testAuthTokens = testAuthTokens;
    _webApp = new WebApp(_databaseName);
    _client = new Lazy<FlurlClient>(() => new FlurlClient(_webApp.CreateClient()));
  }

  public static async Task<TrackClinicDriver> CreateInstance(CancellationToken cancellationToken)
  {
    var adminToken = await SharedKeycloakInstance.GetToken(AdminUser, "password");
    var unprivilegedToken = await SharedKeycloakInstance.GetToken(UnprivilegedUser, "password");
    var artistToken = await SharedKeycloakInstance.GetToken(ArtistUser, "password");
    return new TrackClinicDriver(cancellationToken, new TestAuthTokens(adminToken, unprivilegedToken, artistToken));
  }

  public async ValueTask DisposeAsync()
  {
    await _webApp.DisposeAsync();
    await SharedMongoInstance.DropDatabase(_databaseName, _cancellationToken);
  }

  public async Task Start()
  {
    _webApp.StartServer();
    await WaitForHealthCheck("/health/live", 10.Seconds());
    await WaitForHealthCheck("/health/ready", 10.Seconds());
  }

  public void SimulateMongoFailure()
  {
    var serviceLogicRoot = _webApp.Services.GetRequiredService<ServiceLogicRoot>();
    serviceLogicRoot.MongoClient.Dispose();
  }

  public async Task<HealthResponse> RequestHealthCheck(string urlSegment)
  {
    return new HealthResponse(
      await _client.Value.Request(urlSegment)
        .AllowAnyHttpStatus()
        .GetAsync(cancellationToken: _cancellationToken));
  }

  public async Task<CreateSongProjectResponse> CreateSongProject(string artistName,
    CreateSongRequestDto createSongRequestDto)
  {
    var response = await RequestCreatingSongProject(artistName, createSongRequestDto, r => r);
    response.ShouldBeSuccess();
    return response;
  }

  public async Task<CreateSongProjectResponse> RequestCreatingSongProject(string artistName,
    CreateSongRequestDto createSongRequestDto)
  {
    var response = await RequestCreatingSongProject(artistName, createSongRequestDto, r => r);
    response.ShouldBeSuccess();
    return response;
  }

  public async Task<CreateSongProjectResponse> RequestCreatingSongProject(string artistName,
    CreateSongRequestDto createSongRequestDto, Func<TrackClinicRequestBuilder, TrackClinicRequestBuilder> customize)
  {
    var requestBuilder = new TrackClinicRequestBuilder(
      _client.Value.Request($"/api/v1/artists/{artistName}/songs"),
      _testAuthTokens);
    var flurlResponse = await customize(requestBuilder).Build().PostJsonAsync(
      createSongRequestDto,
      cancellationToken: _cancellationToken);
    return new CreateSongProjectResponse(flurlResponse);
  }

  public async Task<RetrieveProjectDetailsResponse> RetrieveProjectDetails(string artist, string songName)
  {
    var response = await RequestRetrievalOfProjectDetails(artist, songName);
    response.ShouldBeSuccess();
    return response;
  }

  public async Task<RetrieveProjectDetailsResponse> RequestRetrievalOfProjectDetails(string artist, string songName)
  {
    var flurlResponse =
      await new TrackClinicRequestBuilder(
          _client.Value.Request($"/api/v1/artists/{artist}/songs/{songName}"),
          _testAuthTokens)
        .Build()
        .GetAsync(cancellationToken: _cancellationToken);
    return new RetrieveProjectDetailsResponse(flurlResponse);
  }

  public async Task<OpenApiDocumentResponse> GetOpenApiDocument()
  {
    var response = await new TrackClinicRequestBuilder(
        _client.Value.Request("/openapi/v1.json"),
        _testAuthTokens)
      .Build()
      .GetAsync(cancellationToken: _cancellationToken);
    response.StatusCode.Should().Be(200);
    return new OpenApiDocumentResponse(await response.GetStringAsync());
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

    await SetupProxyingRequestsToTestServer(page, "http://localhost/**/*", _cancellationToken);
    await NavigateToPageAndWaitToLoad(page, "http://localhost/scalar/v1", "text=TrackClinic API");

    var title = await page.TitleAsync();
    var bodyText = await page.InnerTextAsync("body");
    return new WebPageResponse(title, bodyText);
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
    return await _client.Value
      .Request(new Uri(routeRequest.Url).PathAndQuery)
      .AllowAnyHttpStatus()
      .SendAsync(new HttpMethod(routeRequest.Method), cancellationToken: ct);
  }
}