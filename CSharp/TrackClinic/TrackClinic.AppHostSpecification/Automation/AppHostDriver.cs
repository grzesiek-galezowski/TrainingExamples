using System.Diagnostics;
using Aspire.Hosting;
using Aspire.Hosting.ApplicationModel;
using Aspire.Hosting.Testing;
using AwesomeAssertions.Extensions;
using Core.Maybe;
using Flurl.Http;
using ServiceSpecificationBuildingBlocks.Operations;
using ServiceSpecificationBuildingBlocks.Zipkin;

namespace TrackClinic.AppHostSpecification.Automation;

public class AppHostDriver : IAsyncDisposable
{
  private static readonly TimeSpan HealthcheckTimeout = 60.Seconds();
  private readonly DistributedApplication _application;
  private readonly CancellationTokenSource _cancellationTokenSource;
  private readonly CancellationToken _cancellationToken;

  private readonly Lazy<FlurlClient> _demoUiClient;
  private readonly Lazy<FlurlClient> _trackClinicClient;
  private readonly Lazy<FlurlClient> _zipkinClient;
  private readonly Lazy<FlurlClient> _grafanaClient;
  private readonly Lazy<FlurlClient> _prometheusClient;
  private readonly Lazy<FlurlClient> _keycloakClient;

  public HealthDriverExtension DemoUiHealth => new(_demoUiClient.Value, "Healthy", _cancellationToken);
  public HealthDriverExtension TrackClinicHealth => new(_trackClinicClient.Value, "Healthy", _cancellationToken);

  public HealthDriverExtension PrometheusHealth => new(
    _prometheusClient.Value,
    "Prometheus Server is Healthy.\n",
    _cancellationToken);

  public HealthDriverExtension ZipkinHealthCheck => new(
    _zipkinClient.Value,
    """
    {
      "status" : "UP",
      "zipkin" : {
        "status" : "UP",
        "details" : {
          "OpenTelemetryHttpCollector{}" : {
            "status" : "UP"
          },
          "InMemoryStorage{}" : {
            "status" : "UP"
          }
        }
      }
    }
    """,
    _cancellationToken);

  public HealthDriverExtension GrafanaHealthCheck => new(
    _grafanaClient.Value,
    """
    {
      "database": "ok",
      "version": "13.3.0",
      "commit": "6f8ce3c693b493043ebcd4787566d2b6b118b0f0"
    }
    """,
    _cancellationToken);

  public GuiDriverExtension Gui => new(DemoUiBaseAddress);

  public ZipkinDriverExtension Zipkin => new(
    _zipkinClient.Value,
    new ZipkinClient(ZipkinBaseAddress),
    _cancellationToken);

  public PrometheusDriverExtension Prometheus => new(_prometheusClient.Value, _cancellationToken);
  public GrafanaDriverExtension Grafana => new(_grafanaClient.Value, _cancellationToken);

  private Uri DemoUiBaseAddress => _application.GetEndpoint("DemoUi", "http");
  private string ZipkinBaseAddress => _application.GetEndpoint("zipkin", "http").ToString();

  public static async Task<AppHostDriver> CreateInstance()
  {
    var cancellationTokenSource = new CancellationTokenSource(
      Debugger.IsAttached ? 5.Minutes() : 2.Minutes());
    var cancellationToken = cancellationTokenSource.Token;

    var builder = await DistributedApplicationTestingBuilder
      .CreateAsync<Program>(["DcpPublisher:RandomizePorts=false"], cancellationToken); //bug maybe the ports CAN be randomized
    builder.Configuration["DcpPublisher:RandomizePorts"] = "false"; //bug why twice?
    ConfigureFixedPorts(builder.Resources);
    builder.Eventing.Subscribe<BeforeStartEvent>((@event, _) =>
    {
      ConfigureFixedPorts(@event.Model.Resources);
      return Task.CompletedTask;
    });

    var application = await builder.BuildAsync(cancellationToken);
    return new AppHostDriver(application, cancellationTokenSource);
  }

  private AppHostDriver(DistributedApplication application, CancellationTokenSource cancellationTokenSource)
  {
    _application = application;
    _cancellationTokenSource = cancellationTokenSource;
    _cancellationToken = _cancellationTokenSource.Token;
    _demoUiClient = CreateLazyClient("DemoUi");
    _trackClinicClient = CreateLazyClient("TrackClinicService");
    _zipkinClient = CreateLazyClient("zipkin");
    _grafanaClient = CreateLazyClient("grafana");
    _prometheusClient = CreateLazyClient("prometheus");
    _keycloakClient = CreateLazyClient("keycloak");
  }

  public async Task Start()
  {
    await _application.StartAsync(_cancellationToken);
    await DemoUiHealth.WaitForHealthCheck("/health/ready", HealthcheckTimeout);
    await TrackClinicHealth.WaitForHealthCheck("/health/ready", HealthcheckTimeout);
    await ZipkinHealthCheck.WaitForHealthCheck("health", HealthcheckTimeout);
    await GrafanaHealthCheck.WaitForHealthCheck("/api/health", HealthcheckTimeout);
    await PrometheusHealth.WaitForHealthCheck("/-/healthy", HealthcheckTimeout);
    await WaitForKeycloakRealmReady(HealthcheckTimeout);
  }

  public async ValueTask DisposeAsync()
  {
    await _application.StopAsync(_cancellationToken);
    await _application.DisposeAsync();
    _cancellationTokenSource.Dispose();
  }

  private Lazy<FlurlClient> CreateLazyClient(string resourceName) =>
    new(() => new FlurlClient(_application.CreateHttpClient(resourceName)));

  private async Task WaitForKeycloakRealmReady(TimeSpan timeout)
  {
    using var cts = CancellationTokenSource.CreateLinkedTokenSource(_cancellationToken);
    cts.CancelAfter(timeout);
    using var timer = new PeriodicTimer(200.Milliseconds());

    while (await timer.WaitForNextTickAsync(cts.Token))
    {
      try
      {
        var response = await _keycloakClient.Value
          .Request("/realms/track-clinic/.well-known/openid-configuration")
          .AllowAnyHttpStatus()
          .GetAsync(cancellationToken: cts.Token);

        if (response.StatusCode == 200)
        {
          return;
        }
      }
      catch
      {
        // Keycloak may still be starting
      }
    }
  }

  private static void ConfigureFixedPorts(IResourceCollection resources)
  {
    //bug really? This is probably for metrics
    SetEndpointPort(resources, "TrackClinicService", 5168);
    SetEndpointPort(resources, "DemoUi", 5138);
    SetEndpointPort(resources, "keycloak", 8080);
    SetEndpointPort(resources, "prometheus", 9090);
    SetEndpointPort(resources, "zipkin", 9411);
    SetEndpointPort(resources, "grafana", 3000);
  }

  private static void SetEndpointPort(IResourceCollection resources, string resourceName, int port)
  {
    var resource = resources.FirstOrDefault(r => r.Name == resourceName).ToMaybe();
    var endpoint = resource.SelectMany(r =>
      r.Annotations.OfType<EndpointAnnotation>().FirstOrDefault(e => e.Name == "http").ToMaybe());

    if (endpoint.HasValue)
    {
      endpoint.Value().Port = port;
    }
  }
}