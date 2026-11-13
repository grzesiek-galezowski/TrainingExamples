using DemoUi.Adapters.Configuration;
using DemoUi.Adapters.Keycloak;
using DemoUi.Logic;
using Flurl.Http;
using Microsoft.Extensions.ServiceDiscovery.Http;

namespace DemoUi;

public class ServiceLogicRoot : IAsyncDisposable
{
  private readonly IFlurlClient _httpClient;

  public ServiceLogicRoot(AppConfig appConfig,
    IServiceDiscoveryHttpMessageHandlerFactory serviceDiscoveryHttpMessageHandlerFactory,
    ILoggerFactory loggerFactory)
  {
    _httpClient = ClientWithServiceDiscovery(
      appConfig.TrackClinic.BaseUrl,
      TimeSpan.FromSeconds(appConfig.TrackClinic.TimeoutSeconds),
      serviceDiscoveryHttpMessageHandlerFactory);
    TrafficGeneratorService = new TrafficGeneratorService(
        new TrackClinicApiClient(
          _httpClient,
          new KeycloakTokenClient(),
          appConfig,
          loggerFactory.CreateLogger<TrackClinicApiClient>()),
      loggerFactory.CreateLogger<TrafficGeneratorService>()
    );
  }

  public TrafficGeneratorService TrafficGeneratorService { get; }

  private static IFlurlClient ClientWithServiceDiscovery(Uri baseUrl, TimeSpan timeout,
    IServiceDiscoveryHttpMessageHandlerFactory serviceDiscoveryHandlerFactory)
  {
    var baseUrlString = baseUrl.ToString();
    var baseUri = new Uri(baseUrlString.EndsWith('/') ? baseUrlString : baseUrlString + "/");
    return new FlurlClient(new HttpClient(serviceDiscoveryHandlerFactory.CreateHandler(new HttpClientHandler()))
    {
      BaseAddress = baseUri,
      Timeout = timeout
    });
  }

  public async ValueTask DisposeAsync()
  {
    _httpClient.Dispose();
    TrafficGeneratorService.Dispose();
  }
}