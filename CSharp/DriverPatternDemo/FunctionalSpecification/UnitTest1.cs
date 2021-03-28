using System;
using System.Threading.Tasks;
using DriverPatternDemo;
using DriverPatternDemo.Controllers;
using Flurl.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using TddXt.AnyRoot.Numbers;
using TddXt.AnyRoot.Strings;
using Xunit;
using static TddXt.AnyRoot.Root;

namespace FunctionalSpecification
{
  public class UnitTest1
  {
    [Fact]
    public async Task Test1() //bug rename
    {
      using var host = Host
        .CreateDefaultBuilder()
        .ConfigureWebHostDefaults(webBuilder =>
        {
          webBuilder
            .UseTestServer()
            .UseEnvironment("Development")
            .UseStartup<Startup>();
        }).Build();

      await host.StartAsync();

      var client = new FlurlClient(host.GetTestClient());

      var postJsonAsync = await client.Request("WeatherForecast")
        .PostJsonAsync(new WeatherForecastDto(
          Any.Instance<DateTime>(),
          Any.Integer(),
          Any.String()));
      var resultDto = await postJsonAsync.GetJsonAsync<ForecastCreationResultDto>();

      await client.Request("WeatherForecast")
        .AppendPathSegment(resultDto.Id)
        .GetJsonAsync<WeatherForecastDto>();
    }
  }
}
