using AwesomeAssertions;
using AwesomeAssertions.Extensions;
using Core.NullableReferenceTypesExtensions;
using Flurl.Http;
using ServiceBuildingBlocks;
using ServiceSpecificationBuildingBlocks.Keycloak;
using ServiceSpecificationBuildingBlocks.Operations;
using System.Collections.Immutable;
using System.Diagnostics;
using TddXt.AnyRoot;
using TddXt.AnyRoot.Strings;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace DemoUiServiceSpecification.Automation;

public class DemoUiDriver : IAsyncDisposable
{
  private readonly CancellationTokenSource _testCaseCancellationTokenSource =
    new(Debugger.IsAttached ? 2.Minutes() : 30.Seconds());

  private readonly CancellationToken _cancellationToken;
  private readonly WireMockServer _trackClinicMock;
  private readonly WebApp _webApp;
  private readonly Lazy<FlurlClient> _client;
  private readonly TimeSpan _accessTokenLifespan = 15.Minutes();
  private readonly string _keycloakRealmName = Any.String(KeycloakConstants.RealmName);

  public GuiDriverExtension Gui => new(GetAppBaseAddress, _cancellationToken);

  public HealthDriverExtension Health => new(_client.Value, _cancellationToken);

  public static Task<DemoUiDriver> CreateInstance()
  {
    return Task.FromResult(new DemoUiDriver());
  }

  private DemoUiDriver()
  {
    _cancellationToken = _testCaseCancellationTokenSource.Token;
    _trackClinicMock = WireMockServer.Start();
    _webApp = new WebApp(_trackClinicMock.Url.OrThrow(), _keycloakRealmName);
    _webApp.UseKestrel(0); //bug what??
    _client = new Lazy<FlurlClient>(() => new FlurlClient(_webApp.CreateClient()));
  }

  public async ValueTask DisposeAsync()
  {
    await _webApp.DisposeAsync();
    _trackClinicMock.Stop();
    _trackClinicMock.Dispose();
    _testCaseCancellationTokenSource.Dispose();
  }

  public async Task Start()
  {
    await SharedKeycloakInstance.ConfigureNewRealm(new TrackClinicKeycloakBootstrap(_keycloakRealmName, _accessTokenLifespan));
    _webApp.StartServer();
    _webApp.CreateClient(); //bug what??
    await Health.WaitForHealthCheck("/health/live", 10.Seconds());
    await Health.WaitForHealthCheck("/health/ready", 10.Seconds());
  }

  public void SetupGetSongDetailsToReturnSuccess(string artist, string songName)
  {
    _trackClinicMock
      .Given(Request.Create().WithPath($"/api/v1/artists/{artist}/songs/{songName}").UsingGet())
      .RespondWith(Response.Create().WithStatusCode(200).WithBody("details"));
  }

  public void SetupCreateSongToReturnSuccess(string artist)
  {
    _trackClinicMock //bug add priorities
      .Given(Request.Create().WithPath($"/api/v1/artists/{artist}/songs").UsingPost())
      .RespondWith(Response.Create().WithStatusCode(201).WithBody("created"));
  }

  public void SetupGetSongDetailsToReturnBadRequest(string artist, string songName)
  {
    _trackClinicMock
      .Given(Request.Create().WithPath($"/api/v1/artists/{artist}/songs/{songName}").UsingGet())
      .RespondWith(Response.Create().WithStatusCode(400).WithBody("bad request"));
  }

  public IReadOnlyList<string> TrackClinicRequestPaths() =>
    [.. _trackClinicMock.LogEntries.Select(x => x.RequestMessage!.Path)]; //bug enable warnings as errors nullable

  public async Task<string> RequestMetrics()
  {
    return await _client.Value.Request("/metrics").GetStringAsync(cancellationToken: _cancellationToken);
  }

  private Uri GetAppBaseAddress()
  {
    return new Uri(_client.Value.BaseUrl);
  }

  public void TrackClinicShouldReceiveRequestsInOrder(ImmutableList<string> expectation)
  {
    TrackClinicRequestPaths().Should().Equal(expectation);
  }
}