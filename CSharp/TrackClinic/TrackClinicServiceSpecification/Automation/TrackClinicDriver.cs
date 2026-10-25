using System.Diagnostics;
using AwesomeAssertions.Extensions;
using Flurl.Http;
using Microsoft.Extensions.DependencyInjection;
using TddXt.AnyRoot;
using TddXt.AnyRoot.Strings;
using TrackClinic;
using TrackClinicServiceSpecification.Automation.Keycloak;

namespace TrackClinicServiceSpecification.Automation;

public class TrackClinicDriver : IAsyncDisposable
{
  private readonly string _databaseName;
  private readonly string _keycloakRealmName;
  private readonly CancellationTokenSource _testCaseCancellationTokenSource =
    new(Debugger.IsAttached ? 10.Minutes() : 30.Seconds());
  private readonly CancellationToken _cancellationToken;
  private readonly WebApp _webApp;
  private readonly Lazy<FlurlClient> _client;
  private TimeSpan _accessTokenLifespan = 15.Minutes();

  public OperationsDriverExtension Operations => new(_client, _keycloakRealmName, _cancellationToken);

  public static async Task<TrackClinicDriver> CreateInstance()
  {
    return new TrackClinicDriver();
  }

  private TrackClinicDriver()
  {
    _databaseName = Any.String("testDb");
    _keycloakRealmName = Any.String(KeycloakConstants.RealmName);
    _cancellationToken = _testCaseCancellationTokenSource.Token;
    _webApp = new WebApp(_databaseName, _keycloakRealmName);
    _client = new Lazy<FlurlClient>(() => new FlurlClient(_webApp.CreateClient()));
  }

  public async ValueTask DisposeAsync()
  {
    await _webApp.DisposeAsync();
    await SharedMongoInstance.DropDatabase(_databaseName, _cancellationToken);
    _testCaseCancellationTokenSource.Dispose();
  }

  public async Task Start()
  {
    await SharedKeycloakInstance.ConfigureNewRealm(new TrackClinicKeycloakBootstrap(_keycloakRealmName, _accessTokenLifespan));
    _webApp.StartServer();
    await WaitForHealthCheck("/health/live", 10.Seconds());
    await WaitForHealthCheck("/health/ready", 10.Seconds());
  }

  public void SetupKeycloakTokenLifespan(TimeSpan lifespan)
  {
    _accessTokenLifespan = lifespan;
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
    return await RequestHealthCheck(urlSegment, b => b);
  }

  public async Task<HealthResponse> RequestHealthCheck(string urlSegment, Func<TrackClinicRequestBuilder, TrackClinicRequestBuilder> customize)
  {
    var builder = await CreateDefaultRequestBuilder(urlSegment, customize);
    return new HealthResponse(
      await builder.Build().GetAsync(cancellationToken: _cancellationToken));
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

  private async Task WaitForHealthCheck(string urlSegment, TimeSpan timeout)
  {
    using var cts = CancellationTokenSource.CreateLinkedTokenSource(_cancellationToken);
    cts.CancelAfter(timeout);
    using var timer = new PeriodicTimer(TimeSpan.FromMilliseconds(100));
    while (await timer.WaitForNextTickAsync(cts.Token))
    {
      if (await (await RequestHealthCheck(urlSegment)).IsHealthy())
      {
        return;
      }
    }
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