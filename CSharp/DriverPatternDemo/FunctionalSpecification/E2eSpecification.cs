using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Flurl.Http;
using Functional.Maybe;
using Functional.Maybe.Just;
using IoCContainerRefactoring;
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

namespace FunctionalSpecification
{
  public class E2ESpecification
  {
    [Fact]
    public async Task ShouldAllowRetrievingReportedForecast()
    {
      //GIVEN
      await using var driver = new AppDriver();
      await driver.StartAsync();
      using var user = new User(driver);

      await user.ReportNewForecast();

      //WHEN
      using var retrievedForecast = await user.RetrieveLastReportedForecast();

      //THEN
      await retrievedForecast.ShouldBeTheSameAs(user.LastReportedForecast());

      //not really part of the scenario...
      driver.Notifications.ShouldIncludeNotificationAbout(user.LastReportedForecast());
    }

    [Fact]
    public async Task ShouldAllowRetrievingReportsFromAParticularUser()
    {
      //GIVEN
      await using var driver = new AppDriver();
      await driver.StartAsync();
      using var user1 = new User(driver);
      using var user2 = new User(driver);

      await user1.ReportNewForecast();
      await user1.ReportNewForecast();
      await user2.ReportNewForecast();

      //WHEN
      using var allForecastsReportedByUser1 = await user1.RetrieveAllReportedForecasts();

      //THEN
      await allForecastsReportedByUser1.ShouldConsistOf(user1.AllReportedForecasts());
    }

    [Fact]
    public async Task ShouldRejectForecastReportAsBadRequestWhenTemperatureIsBelowAllowedMinimum()
    {
      //GIVEN
      await using var driver = new AppDriver();
      await driver.StartAsync();
      using var user = new User(driver);

      //WHEN
      using var reportForecastResponse = 
        await user.AttemptToReportNewForecast(forecast => forecast with {TemperatureC = -101});

      //THEN
      reportForecastResponse.ShouldBeRejectedAsBadRequest();
      driver.Notifications.ShouldNotIncludeAnything();
    }
  }

  public class User : IDisposable
  {
    private readonly AppDriver _driver;
    private readonly string _tenantId1 = Any.String();
    private readonly string _userId1 = Any.String();
    private readonly List<WeatherForecastReportBuilder> _reportedForecasts = new();
    private readonly List<ReportForecastResponse> _forecastCreationResponses = new();

    public User(AppDriver driver)
    {
      _driver = driver;
    }

    public static WeatherForecastReportBuilder ForecastReport() => new();

    public async Task ReportNewForecast()
    {
      await ReportNewForecast(_ => _);
    }

    public async Task ReportNewForecast(Func<WeatherForecastReportBuilder, WeatherForecastReportBuilder> customize)
    {
      var reportForecastResponse = await AttemptToReportNewForecast(customize);
      _forecastCreationResponses.Add(reportForecastResponse);
    }

    public async Task<ReportForecastResponse> AttemptToReportNewForecast(Func<WeatherForecastReportBuilder, WeatherForecastReportBuilder> customize)
    {
      var forecast = customize(CreateForecast());
      _reportedForecasts.Add(forecast);
      return await _driver.WeatherForecastApi.AttemptToReportForecast(forecast);
    }

    public async Task<RetrievedForecasts> RetrieveAllReportedForecasts()
    {
      return await _driver.WeatherForecastApi.GetReportedForecastsFrom(_userId1, _tenantId1);
    }

    public WeatherForecastReportBuilder[] AllReportedForecasts()
    {
      return _reportedForecasts.ToArray();
    }

    private WeatherForecastReportBuilder CreateForecast()
    {
      return ForecastReport() with {UserId = _userId1, TenantId = _tenantId1};
    }

    public void Dispose()
    {
      foreach (var response in _forecastCreationResponses)
      {
        response.Dispose();
      }
    }

    public ReportForecastResponse LastReportedForecastResponse()
    {
      return _forecastCreationResponses.Last();
    }

    public async Task<RetrievedForecast> RetrieveLastReportedForecast()
    {
      var retrievedForecast = await _driver.WeatherForecastApi.GetReportedForecastBy(
        await LastReportedForecastResponse().GetId());
      return retrievedForecast;
    }

    public WeatherForecastReportBuilder LastReportedForecast()
    {
      return _reportedForecasts.Last();
    }
  }

  public class AppDriver : IAsyncDisposable
  {
    private Maybe<IHost> _host;
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
      => new(HttpClient);
  }

  public class NotificationsDriverExtension
  {
    private readonly WireMockServer _wireMockServer;

    public NotificationsDriverExtension(WireMockServer wireMockServer)
    {
      _wireMockServer = wireMockServer;
    }

    public void ShouldIncludeNotificationAbout(WeatherForecastReportBuilder builder)
    {
      var dto = builder.Build();
      _wireMockServer.LogEntries.Should().ContainSingle(entry =>
        entry.RequestMatchResult.IsPerfectMatch
        && entry.RequestMessage.Path == "/notifications"
        && entry.RequestMessage.Method == "POST"
        && JsonConvert.DeserializeObject<WeatherForecastSuccessfullyReportedEventDto>(
          entry.RequestMessage.Body) == new WeatherForecastSuccessfullyReportedEventDto(
          dto.TenantId,
          dto.UserId,
          dto.TemperatureC));
    }

    public void ShouldNotIncludeAnything()
    {
      _wireMockServer.LogEntries.Should().BeEmpty();
    }
  }

  public class WeatherForecastApiDriverExtension
  {
    private readonly IFlurlClient _httpClient;

    public WeatherForecastApiDriverExtension(
      IFlurlClient httpClient)
    {
      _httpClient = httpClient;
    }

    public async Task<ReportForecastResponse> AttemptToReportForecast(WeatherForecastReportBuilder builder)
    {
      var httpResponse = await AttemptToReportForecastViaHttp(builder);
      return new ReportForecastResponse(httpResponse);
    }

    private async Task<IFlurlResponse> AttemptToReportForecastViaHttp(WeatherForecastReportBuilder builder)
    {
      var httpResponse = await _httpClient
        .Request("WeatherForecast")
        .AllowAnyHttpStatus()
        .PostJsonAsync(builder.Build());
      return httpResponse;
    }

    public async Task<RetrievedForecast> GetReportedForecastBy(Guid id)
    {
      var httpResponse = await _httpClient.Request("WeatherForecast")
        .AppendPathSegment(id)
        .AllowAnyHttpStatus()
        .GetAsync();

      return new RetrievedForecast(httpResponse);
    }

    public async Task<RetrievedForecasts> GetReportedForecastsFrom(string userId, string tenantId)
    {
      var httpResponse = await _httpClient.Request("WeatherForecast")
        .AppendPathSegment(tenantId)
        .AppendPathSegment(userId)
        .AllowAnyHttpStatus()
        .GetAsync();

      var reportedForecasts = new RetrievedForecasts(httpResponse);
      reportedForecasts.ShouldIndicateSuccess();
      return reportedForecasts;
    }
  }

  public class RetrievedForecasts : IDisposable
  {
    private readonly IFlurlResponse _flurlResponse;

    public RetrievedForecasts(IFlurlResponse flurlResponse)
    {
      _flurlResponse = flurlResponse;
    }

    public void Dispose()
    {
      _flurlResponse.Dispose();
    }

    public void ShouldIndicateSuccess()
    {
      _flurlResponse.StatusCode.Should().Be((int) HttpStatusCode.OK);
    }

    public async Task ShouldConsistOf(params WeatherForecastReportBuilder[] builders)
    {
      var expectedDtos = builders.Select(b => b.Build());
      var actualDtos = await _flurlResponse.GetJsonAsync<IEnumerable<WeatherForecastDto>>();

      actualDtos.Should().Equal(expectedDtos);
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

    public void ShouldBeSuccessful()
    {
      _httpResponse.StatusCode.Should().Be((int) HttpStatusCode.OK);
    }

    public async Task<Guid> GetId()
    {
      return (await _httpResponse.GetJsonAsync<ForecastCreationResultDto>()).Id;
    }
  }

  public class RetrievedForecast : IDisposable
  {
    private readonly IFlurlResponse _httpResponse;

    public RetrievedForecast(IFlurlResponse httpResponse)
    {
      _httpResponse = httpResponse;
    }

    public async Task ShouldBeTheSameAs(WeatherForecastReportBuilder expectedBuilder)
    {
      _httpResponse.StatusCode.Should().Be((int) HttpStatusCode.OK);
      var weatherForecastDto = await _httpResponse.GetJsonAsync<WeatherForecastDto>();
      weatherForecastDto.Should().BeEquivalentTo(expectedBuilder.Build());
    }

    public void Dispose()
    {
      _httpResponse.Dispose();
    }
  }

  public record WeatherForecastReportBuilder
  {
    public string TenantId { private get; init; } = Any.String();
    public string UserId { private get; init; } = Any.String();
    public DateTime Time { private get; init; } = Any.Instance<DateTime>();
    public int TemperatureC { private get; init; } = Any.Integer();
    public string Summary { private get; init; } = Any.String();

    public WeatherForecastDto Build()
    {
      return new(
        TenantId, 
        UserId, 
        Time,
        TemperatureC,
        Summary);
    }
  }
}