using System.Net.Http.Json;
using CompositionMaintenanceExample;
using CompositionMaintenanceExample.Dto;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;

namespace Tests
{
    public class Tests
  {
    [Test]
    public async Task Test1()
    {
      await using var webApp = new WebApplicationFactory<Program>()
        .WithWebHostBuilder(b => b.UseTestServer(o => o.PreserveExecutionContext = true));
      using var httpClient = webApp.CreateClient();

      await TestSaveAndGet(httpClient, "WeatherForecast1", new WeatherForecastDto1(DateOnly.MinValue, 123, "lol"));
      await TestSaveAndGet(httpClient, "WeatherForecast2", new WeatherForecastDto2(DateOnly.MinValue, 123, "lol"));
      await TestSaveAndGet(httpClient, "WeatherForecast3", new WeatherForecastDto3(DateOnly.MinValue, 123, "lol"));
      await TestSaveAndGet(httpClient, "WeatherForecast4", new WeatherForecastDto4(DateOnly.MinValue, 123, "lol"));
      await TestSaveAndGet(httpClient, "WeatherForecast5", new WeatherForecastDto5(DateOnly.MinValue, 123, "lol"));
      await TestSaveAndGet(httpClient, "WeatherForecast6", new WeatherForecastDto6(DateOnly.MinValue, 123, "lol"));
      await TestSaveAndGet(httpClient, "WeatherForecast7", new WeatherForecastDto7(DateOnly.MinValue, 123, "lol"));
      await TestSaveAndGet(httpClient, "WeatherForecast8", new WeatherForecastDto8(DateOnly.MinValue, 123, "lol"));
      await TestSaveAndGet(httpClient, "WeatherForecast9", new WeatherForecastDto9(DateOnly.MinValue, 123, "lol"));
      await TestSaveAndGet(httpClient, "WeatherForecast10", new WeatherForecastDto10(DateOnly.MinValue, 123, "lol"));
      await TestSaveAndGet(httpClient, "WeatherForecast11", new WeatherForecastDto11(DateOnly.MinValue, 123, "lol"));
      await TestSaveAndGet(httpClient, "WeatherForecast12", new WeatherForecastDto12(DateOnly.MinValue, 123, "lol"));
      await TestSaveAndGet(httpClient, "WeatherForecast13", new WeatherForecastDto13(DateOnly.MinValue, 123, "lol"));
      await TestSaveAndGet(httpClient, "WeatherForecast14", new WeatherForecastDto14(DateOnly.MinValue, 123, "lol"));
      await TestSaveAndGet(httpClient, "WeatherForecast15", new WeatherForecastDto15(DateOnly.MinValue, 123, "lol"));
      await TestSaveAndGet(httpClient, "WeatherForecast16", new WeatherForecastDto16(DateOnly.MinValue, 123, "lol"));
      await TestSaveAndGet(httpClient, "WeatherForecast17", new WeatherForecastDto17(DateOnly.MinValue, 123, "lol"));
      await TestSaveAndGet(httpClient, "WeatherForecast18", new WeatherForecastDto18(DateOnly.MinValue, 123, "lol"));
      await TestSaveAndGet(httpClient, "WeatherForecast19", new WeatherForecastDto19(DateOnly.MinValue, 123, "lol"));
      await TestSaveAndGet(httpClient, "WeatherForecast20", new WeatherForecastDto20(DateOnly.MinValue, 123, "lol"));
    }

    private static async Task TestSaveAndGet<TInput>(HttpClient httpClient, string requestUri, TInput input)
    {
      await httpClient.PostAsJsonAsync(requestUri, input);
      var response = await httpClient.GetFromJsonAsync<TInput>(requestUri);
      Assert.That(response, Is.EqualTo(input));
    }
  }
}