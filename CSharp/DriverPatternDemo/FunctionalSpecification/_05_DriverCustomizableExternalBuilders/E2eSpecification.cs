using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DriverPatternDemo;
using FluentAssertions;
using Flurl.Http;
using Functional.Maybe;
using Functional.Maybe.Just;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using TddXt.AnyExtensibility;
using TddXt.AnyRoot.Numbers;
using TddXt.AnyRoot.Strings;
using TddXt.AnyRoot.Time;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using Xunit;
using static TddXt.AnyRoot.Root;

namespace FunctionalSpecification._05_DriverCustomizableExternalBuilders
{
  public class E2ESpecification
  {
    [Fact]
    public async Task ShouldAllowRetrievingReportedForecast()
    {
      //GIVEN
      await using var driver = new AppDriver();
      await driver.StartAsync();
      var weatherForecastDto = Any.WeatherForecast();

      await driver.WeatherForecastApi.Report(weatherForecastDto);

      //WHEN
      using var retrievedForecast = await driver.WeatherForecastApi.GetReportedForecast();

      //THEN
      await retrievedForecast.ShouldBeTheSameAs(weatherForecastDto); //bug builder

      //not really part of the scenario...
      driver.Notifications.ShouldIncludeNotificationAbout(weatherForecastDto);
    }

    [Fact]
    public async Task ShouldRejectForecastReportAsBadRequestWhenTemperatureIsLessThanMinus100()
    {
      //GIVEN
      await using var driver = new AppDriver();
      await driver.StartAsync();

      //WHEN
      using var reportForecastResponse = await driver.WeatherForecastApi
        .AttemptToReportForecast(Any.WeatherForecast() with {TemperatureC = -101});

      //THEN
      reportForecastResponse.ShouldBeRejectedAsBadRequest();
      driver.Notifications.ShouldNotIncludeAnything();
    }
  }

  //Two deficiencies of this driver:
  //1) _lastInput lifetime is managed internally
  //2) _lastOutput lifetime is managed internally
  //3) more than one "entity"

  public class AppDriver : IAsyncDisposable, IAppDriverContext
  {
    private Maybe<IHost> _host;
    private Maybe<ForecastCreationResultDto> _lastReportResult; //not pretty
    private readonly WireMockServer _notificationRecipient;

    private FlurlClient HttpClient => new(_host.Value.GetTestClient());

    public AppDriver()
    {
      _notificationRecipient = WireMockServer.Start();
    }

    public async Task StartAsync()
    {
      _notificationRecipient.Given(
        Request.Create()
          .WithPath("/notifications")
          .UsingPost()).RespondWith(
        Response.Create()
          .WithStatusCode(HttpStatusCode.OK));

      _host = Host
        .CreateDefaultBuilder()
        .ConfigureWebHostDefaults(webBuilder =>
        {
          webBuilder
            .UseTestServer()
            .ConfigureAppConfiguration(appConfig =>
            {
              appConfig.AddInMemoryCollection(new Dictionary<string, string>
              {
                ["NotificationsConfiguration:BaseUrl"] = _notificationRecipient.Urls.Single()
              });
            })
            .UseEnvironment("Development")
            .UseStartup<Startup>();
        }).Build().Just();
      await _host.Value.StartAsync();
    }

    //Note explicit implementation
    void IAppDriverContext.SaveAsLastForecastReportResult(ForecastCreationResultDto dto)
    {
      _lastReportResult = dto.Just();
    }

    public async ValueTask DisposeAsync()
    {
      await _host.DoAsync(async host =>
      {
        await host.StopAsync();
        host.Dispose();
      });
      _notificationRecipient.Dispose();
    }

    public NotificationsDriverExtension Notifications =>
      new(_notificationRecipient);

    public WeatherForecastApiDriverExtension WeatherForecastApi
      => new(this, HttpClient, _lastReportResult);
  }

  public class NotificationsDriverExtension
  {
    private readonly WireMockServer _wireMockServer;

    public NotificationsDriverExtension(WireMockServer wireMockServer)
    {
      _wireMockServer = wireMockServer;
    }

    public void ShouldIncludeNotificationAbout(WeatherForecastDto weatherForecastDto)
    {
      _wireMockServer.LogEntries.Should().ContainSingle(entry =>
        entry.RequestMatchResult.IsPerfectMatch
        && entry.RequestMessage.Path == "/notifications"
        && entry.RequestMessage.Method == "POST"
        && JsonConvert.DeserializeObject<WeatherForecastSuccessfullyReportedEventDto>(
          entry.RequestMessage.Body) == new WeatherForecastSuccessfullyReportedEventDto(
          weatherForecastDto.TenantId,
          weatherForecastDto.UserId,
          weatherForecastDto.TemperatureC));
    }

    public void ShouldNotIncludeAnything()
    {
      _wireMockServer.LogEntries.Should().BeEmpty();
    }
  }

  public interface IAppDriverContext
  {
    void SaveAsLastForecastReportResult(ForecastCreationResultDto jsonResponse);
  }

  public class WeatherForecastApiDriverExtension
  {
    private readonly IFlurlClient _httpClient;
    private readonly Maybe<ForecastCreationResultDto> _lastReportResult;
    private readonly IAppDriverContext _driverContext;

    public WeatherForecastApiDriverExtension(
      IAppDriverContext driverContext,
      IFlurlClient httpClient,
      Maybe<ForecastCreationResultDto> lastReportResult)
    {
      _driverContext = driverContext;
      _httpClient = httpClient;
      _lastReportResult = lastReportResult;
    }

    public async Task Report(WeatherForecastDto weatherForecastDto)
    {
      var httpResponse = await AttemptToReportForecastViaHttp(weatherForecastDto);
      var jsonResponse = await httpResponse.GetJsonAsync<ForecastCreationResultDto>();
      _driverContext.SaveAsLastForecastReportResult(jsonResponse);
    }

    public async Task<ReportForecastResponse> AttemptToReportForecast(WeatherForecastDto weatherForecastDto) //bug builder instead of DTO
    {
      var httpResponse = await AttemptToReportForecastViaHttp(weatherForecastDto);
      return new ReportForecastResponse(httpResponse);
    }

    private async Task<IFlurlResponse> AttemptToReportForecastViaHttp(WeatherForecastDto weatherForecastDto)
    {
      var httpResponse = await _httpClient
        .Request("WeatherForecast")
        .AllowAnyHttpStatus()
        .PostJsonAsync(weatherForecastDto);
      return httpResponse;
    }

    public async Task<RetrievedForecast> GetReportedForecast()
    {
      var httpResponse = await _httpClient.Request("WeatherForecast")
        .AppendPathSegment(_lastReportResult.Value.Id)
        .AllowAnyHttpStatus()
        .GetAsync();

      return new RetrievedForecast(httpResponse);
    }
  }

  public class ReportForecastResponse : IDisposable
  {
    private readonly IFlurlResponse _httpResponse;

    public ReportForecastResponse(IFlurlResponse httpResponse)
    {
      _httpResponse = httpResponse;
    }

    public void Dispose()
    {
      _httpResponse.Dispose();
    }

    public void ShouldBeRejectedAsBadRequest()
    {
      _httpResponse.StatusCode.Should().Be((int) HttpStatusCode.BadRequest);
    }
  }

  public class RetrievedForecast : IDisposable
  {
    private readonly IFlurlResponse _httpResponse;

    public RetrievedForecast(IFlurlResponse httpResponse)
    {
      _httpResponse = httpResponse;
    }

    public async Task ShouldBeTheSameAs(WeatherForecastDto expected)
    {
      _httpResponse.StatusCode.Should().Be((int) HttpStatusCode.OK);
      var weatherForecastDto = await _httpResponse.GetJsonAsync<WeatherForecastDto>();
      weatherForecastDto.Should().BeEquivalentTo(expected);
    }

    public void Dispose()
    {
      _httpResponse.Dispose();
    }
  }

  internal static class AnyExtensions //bug rename
  {
    private static readonly string TenantId = Any.String();
    private static readonly string UserId = Any.String();

    public static WeatherForecastDto WeatherForecast(this BasicGenerator gen)
    {
      return new(
        TenantId, 
        UserId, 
        gen.DateTime(), 
        gen.Integer(), 
        gen.String());
    }
  }
}