using AwesomeAssertions.Extensions;
using Core.NullableReferenceTypesExtensions;
using DemoUi;
using Flurl.Http;
using ServiceBuildingBlocks;
using ServiceSpecificationBuildingBlocks.Keycloak;
using ServiceSpecificationBuildingBlocks.Operations;
using System.Collections.Concurrent;
using System.Diagnostics;
using TddXt.AnyRoot;
using TddXt.AnyRoot.Strings;
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
  private int _trackClinicMockPriority = int.MaxValue;
  private TestActivityListener _activityListener = new TestActivityListener(Telemetry.ScenarioActivityName);

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

// bug
public sealed class TestActivityListener : IDisposable
{
  private readonly ActivityListener _listener;

  public ConcurrentBag<Activity> Activities { get; } = new();

  public TestActivityListener(string sourceName)
  {
    _listener = new ActivityListener
    {
      ShouldListenTo = source => true,
      Sample = (ref ActivityCreationOptions<ActivityContext> _) =>
          ActivitySamplingResult.AllDataAndRecorded,
      ActivityStarted = activity => Activities.Add(activity),

      ActivityStopped = _ => { }
    };

    ActivitySource.AddActivityListener(_listener);
  }

  public void Dispose()
  {
    _listener.Dispose();
  }
}