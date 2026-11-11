using AwesomeAssertions.Extensions;
using Core.NullableReferenceTypesExtensions;
using Flurl.Http;
using ServiceBuildingBlocks;
using ServiceSpecificationBuildingBlocks.Keycloak;
using ServiceSpecificationBuildingBlocks.Operations;
using TddXt.AnyRoot;
using TddXt.AnyRoot.Strings;
using WireMock.Server;
using Serilog.Sinks.InMemory;
using ServiceSpecificationBuildingBlocks;

namespace DemoUiServiceSpecification.Automation;

public class DemoUiDriver : IAsyncDisposable
{
  private readonly CancellationTokenSource _testCaseCancellationTokenSource =
    TestCancellationTokens.CreateCancellationTokenSource();

  private readonly CancellationToken _cancellationToken;
  private readonly WireMockServer _trackClinicMock;
  private readonly WebApp _webApp;
  private readonly Lazy<FlurlClient> _client;
  private readonly InMemorySink _inMemorySink;
  private readonly TimeSpan _accessTokenLifespan = 15.Minutes();
  private readonly string _keycloakRealmName = Any.String(KeycloakConstants.RealmName);
  private int _trackClinicMockPriority = int.MaxValue;
  private readonly TestActivityListener _activityListener = new();

  public GuiDriverExtension Gui => new(GetAppBaseAddress, _cancellationToken);

  public HealthDriverExtension Health => new(_client.Value, _cancellationToken);

  public TrackClinicDriverExtension TrackClinic => new(_trackClinicMock, _trackClinicMockPriority--);

  public static Task<DemoUiDriver> CreateInstance()
  {
    return Task.FromResult(new DemoUiDriver());
  }

  private DemoUiDriver()
  {
    _cancellationToken = _testCaseCancellationTokenSource.Token;
    _trackClinicMock = WireMockServer.Start();
    _inMemorySink = new InMemorySink();
    _webApp = new WebApp(_trackClinicMock.Url.OrThrow(), _keycloakRealmName, _inMemorySink);
    _webApp.UseKestrel(0); //bug what??
    _client = new Lazy<FlurlClient>(() => new FlurlClient(_webApp.CreateClient()));
  }

  public async ValueTask DisposeAsync()
  {
    foreach (var @event in _inMemorySink.Snapshot().LogEvents)
    {
      await TestContext.Out.WriteLineAsync(@event.RenderMessage(), _cancellationToken);
    }
    await _webApp.DisposeAsync();
    _trackClinicMock.Stop();
    _trackClinicMock.Dispose();
    _testCaseCancellationTokenSource.Dispose();
    _activityListener.Dispose();
  }

  public async Task Start()
  {
    await SharedKeycloakInstance.ConfigureNewRealm(new TrackClinicKeycloakBootstrap(_keycloakRealmName, _accessTokenLifespan));
    _webApp.StartServer();
    _webApp.CreateClient(); //bug what??
    await Health.WaitForHealthCheck("/health/live", 10.Seconds());
    await Health.WaitForHealthCheck("/health/ready", 10.Seconds());
  }

  public async Task<string> RequestMetrics()
  {
    return await _client.Value.Request("/metrics").GetStringAsync(cancellationToken: _cancellationToken);
  }

  private Uri GetAppBaseAddress()
  {
    return new Uri(_client.Value.BaseUrl);
  }
}