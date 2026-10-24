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
  private readonly string _databaseName;
  private readonly string _keycloakRealmName;
  private readonly CancellationToken _cancellationToken;
  private readonly WebApp _webApp;
  private readonly Lazy<FlurlClient> _client;
  private TimeSpan _tokenLifespan = 15.Minutes();

  public static async Task<TrackClinicDriver> CreateInstance(CancellationToken cancellationToken)
  {
    return new TrackClinicDriver(cancellationToken);
  }

  private TrackClinicDriver(CancellationToken cancellationToken)
  {
    _databaseName = Any.String("testDb");
    _keycloakRealmName = Any.String(KeycloakConstants.RealmName);
    _cancellationToken = cancellationToken;
    _webApp = new WebApp(_databaseName, _keycloakRealmName);
    _client = new Lazy<FlurlClient>(() => new FlurlClient(_webApp.CreateClient()));
  }

  public async ValueTask DisposeAsync()
  {
    await _webApp.DisposeAsync();
    await SharedMongoInstance.DropDatabase(_databaseName, _cancellationToken);
  }

  public async Task Start()
  {
    await SharedKeycloakInstance.ConfigureNewRealm(new TrackClinicKeycloakBootstrap(_keycloakRealmName, _tokenLifespan));
    _webApp.StartServer();
    await WaitForHealthCheck("/health/live", 10.Seconds());
    await WaitForHealthCheck("/health/ready", 10.Seconds());
  }

  public void SetupKeycloakTokenLifespan(TimeSpan lifespan)
  {
    _tokenLifespan = lifespan;
  }

  public void SetupGracePeriod(TimeSpan gracePeriod)
  {
    _webApp.OverrideConfig("Keycloak:GracePeriod", gracePeriod.ToString());
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
    var builder = await CreateDefaultRequestBuilder($"/api/v1/artists/{artistName}/songs", customize);
    var flurlResponse = await builder.Build().PostJsonAsync(
      createSongRequestDto,
      cancellationToken: _cancellationToken);
    return new CreateSongProjectResponse(flurlResponse);
  }

  public async Task<RetrieveProjectDetailsResponse> RetrieveProjectDetails(string artist, string songName)
  {
    var response = await RequestRetrievalOfProjectDetails(artist, songName, r => r);
    response.ShouldBeSuccess();
    return response;
  }

  public async Task<RetrieveProjectDetailsResponse> RequestRetrievalOfProjectDetails(string artist, string songName)
  {
    return await RequestRetrievalOfProjectDetails(artist, songName, r => r);
  }

  public async Task<RetrieveProjectDetailsResponse> RequestRetrievalOfProjectDetails(string artist, string songName,
    Func<TrackClinicRequestBuilder, TrackClinicRequestBuilder> customize)
  {
    var customizedBuilder = await CreateDefaultRequestBuilder($"/api/v1/artists/{artist}/songs/{songName}", customize);
    var flurlResponse = await customizedBuilder.Build().GetAsync(cancellationToken: _cancellationToken);
    return new RetrieveProjectDetailsResponse(flurlResponse);
  }

  public async Task<OpenApiDocumentResponse> GetOpenApiDocument()
  {
    var response = await new TrackClinicRequestBuilder(
        _client.Value.Request("/openapi/v1.json"),
        await SharedKeycloakInstance.GetTestAuthTokens(_keycloakRealmName))
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

  private async Task<TrackClinicRequestBuilder> CreateDefaultRequestBuilder(string urlSegments,
    Func<TrackClinicRequestBuilder, TrackClinicRequestBuilder> customize)
  {
    var requestBuilder = new TrackClinicRequestBuilder(
      _client.Value.Request(urlSegments),
      await SharedKeycloakInstance.GetTestAuthTokens(_keycloakRealmName));
    var customizedBuilder = customize(requestBuilder);
    await customizedBuilder.WaitIfNeeded(_cancellationToken);
    return customizedBuilder;
  }
}