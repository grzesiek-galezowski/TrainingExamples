using Application;
using Flurl.Http;
using Polly;

namespace WeatherAppLogicSeparationSpecification.Automation;

public class Driver : IAsyncDisposable
{
  private readonly WebApp _app = new();
  private FlurlClient? _httpClient;

  public async Task<SubscriptionApiTestResponse> RequestSubscribingToWeatherNotifications(SubscriptionRequestDto subscriptionRequestDto)
  {
    var response = await _httpClient.Request("/weatherforecast").AllowAnyHttpStatus()
      .PostJsonAsync(subscriptionRequestDto);
    return new SubscriptionApiTestResponse(response);
  }

  public async Task Start()
  {
    _httpClient = new FlurlClient(_app.CreateClient());

    await Policy.Handle<FlurlHttpException>()
      .WaitAndRetryAsync(3, _ => TimeSpan.FromSeconds(1))
      .ExecuteAsync(async () =>
      {
        await _httpClient.Request("/healthy").GetAsync();
      });
  }

  public async ValueTask DisposeAsync()
  {
    await _app.DisposeAsync();
  }
}