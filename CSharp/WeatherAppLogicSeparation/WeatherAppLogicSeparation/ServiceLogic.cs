using Application;
using WeatherAppLogicSeparation.Controllers;
using ILogger = Serilog.ILogger;

namespace WeatherAppLogicSeparation;

/// <summary>
/// Service logic composition root
/// </summary>
public class ServiceLogic(ILogger logger)
{
  private readonly FakeMetrics _fakeMetrics = new FakeMetrics();

  public WeatherForecastController CreateWeatherController()
  {
    return new WeatherForecastController(new ApplicationLogic(new DeviceSubscriptionHttpApi(), new HttpDeviceApi(), logger),
      _fakeMetrics,
      logger);
  }
  //bug validation
}