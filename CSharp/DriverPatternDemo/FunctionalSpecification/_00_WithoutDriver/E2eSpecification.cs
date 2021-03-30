using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DriverPatternDemo;
using FluentAssertions;
using Flurl.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using TddXt.AnyRoot.Numbers;
using TddXt.AnyRoot.Strings;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using Xunit;
using static TddXt.AnyRoot.Root;

namespace FunctionalSpecification._00_WithoutDriver
{
  public class E2ESpecification
  {
    [Fact]
    public async Task ShouldAllowRetrievingReportedForecast()
    {
      var tenantId = Any.String();
      var userId = Any.String();

      using var notificationRecipient = WireMockServer.Start();
      notificationRecipient.Given(
        Request.Create()
          .WithPath("/notifications")
          .UsingPost()).RespondWith(
        Response.Create()
          .WithStatusCode(HttpStatusCode.OK));
      
      var inputForecastDto = new WeatherForecastDto(
        tenantId, 
        userId, 
        Any.Instance<DateTime>(),
        Any.Integer(),
        Any.String());
      using var host = Host
        .CreateDefaultBuilder()
        .ConfigureWebHostDefaults(webBuilder =>
        {
          webBuilder
            .UseTestServer()
            .ConfigureAppConfiguration(appConfig =>
            {
              appConfig.AddInMemoryCollection(new Dictionary<string, string>
              {
                ["NotificationsConfiguration:BaseUrl"] = notificationRecipient.Urls.Single()
              });
            })
            .UseEnvironment("Development")
            .UseStartup<Startup>();
        }).Build();

      await host.StartAsync();

      var client = new FlurlClient(host.GetTestClient());

      using var postJsonAsync = await client.Request("WeatherForecast")
        .PostJsonAsync(inputForecastDto);
      var resultDto = await postJsonAsync.GetJsonAsync<ForecastCreationResultDto>();

      using var httpResponse = await client.Request("WeatherForecast")
        .AppendPathSegment(resultDto.Id)
        .AllowAnyHttpStatus()
        .GetAsync();

      var weatherForecastDto = await httpResponse.GetJsonAsync<WeatherForecastDto>();
      weatherForecastDto.Should().BeEquivalentTo(inputForecastDto);

      notificationRecipient.LogEntries.Should().ContainSingle(entry =>
        entry.RequestMatchResult.IsPerfectMatch
        && entry.RequestMessage.Path == "/notifications"
        && entry.RequestMessage.Method == "POST"
        && JsonConvert.DeserializeObject<WeatherForecastSuccessfullyReportedEventDto>(
           entry.RequestMessage.Body) == new WeatherForecastSuccessfullyReportedEventDto(
           tenantId, userId, inputForecastDto.TemperatureC));
    }
  }
}
