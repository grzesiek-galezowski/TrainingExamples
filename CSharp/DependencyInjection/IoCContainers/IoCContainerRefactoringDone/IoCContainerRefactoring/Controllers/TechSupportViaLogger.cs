using Microsoft.Extensions.Logging;

namespace IoCContainerRefactoring.Controllers
{
  public interface ITechSupport
  {
    void NotifyLoaded(object source,
      PersistentWeatherForecastDto persistentWeatherForecastDto);

    void NotifyTemperatureTooLowIn(object source,
      WeatherForecastDto forecastDto);

    void NotifyReportWeatherStarted(object source);
  }

  public class TechSupportViaLogger : ITechSupport
  {
    private readonly ILoggerFactory _loggerFactory;

    public TechSupportViaLogger(ILoggerFactory loggerFactory)
    {
      _loggerFactory = loggerFactory;
    }

    public void NotifyLoaded(object source,
      PersistentWeatherForecastDto persistentWeatherForecastDto)
    {
      _loggerFactory.CreateLogger(source.GetType())
        .LogInformation("Loaded " + persistentWeatherForecastDto);
    }

    public void NotifyTemperatureTooLowIn(object source,
      WeatherForecastDto forecastDto)
    {
      _loggerFactory.CreateLogger(source.GetType())
        .LogWarning($"Validation failed, because the temperature reported is {forecastDto.TemperatureC}");
    }

    public void NotifyReportWeatherStarted(object source)
    {
      _loggerFactory.CreateLogger(source.GetType()).LogInformation("Started reporting weather forecast");
    }
  }
}