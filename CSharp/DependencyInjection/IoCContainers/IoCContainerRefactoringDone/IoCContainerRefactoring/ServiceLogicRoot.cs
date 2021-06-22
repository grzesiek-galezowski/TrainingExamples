using System;
using Flurl.Http;
using IoCContainerRefactoring.Controllers;
using Microsoft.Extensions.Logging;

namespace IoCContainerRefactoring
{
  public class ServiceLogicRoot : IDisposable
  {
    private readonly TechSupportViaLogger _techSupportViaLogger;
    private readonly WeatherCommandFactory _weatherCommandFactory;
    private readonly FlurlClient _flurlClient;

    public ServiceLogicRoot(string eventDestinationUrl, ILoggerFactory loggerFactory)
    {
      _techSupportViaLogger = new TechSupportViaLogger(loggerFactory);
      _flurlClient = new FlurlClient(
        eventDestinationUrl
      );
      _weatherCommandFactory = new WeatherCommandFactory(
        _techSupportViaLogger,
        new EventPipe(_flurlClient),
        new PersistentWeatherForecastDtoFactory(),
        new WeatherForecastDtoFactory(),
        new IdGenerator()
      );
    }

    public WeatherForecastController CreateWeatherForecastController(
      WeatherForecastDbContext weatherForecastDbContext)
    {
      return new WeatherForecastController(
        new WeatherForecastDao(
          weatherForecastDbContext),
        _weatherCommandFactory,
        _techSupportViaLogger);
    }

    public void Dispose()
    {
      _flurlClient.Dispose();
    }
  }
}