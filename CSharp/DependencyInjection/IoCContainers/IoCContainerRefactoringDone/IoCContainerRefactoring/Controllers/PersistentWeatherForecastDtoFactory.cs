using System;

namespace IoCContainerRefactoring.Controllers
{
  public interface IPersistentWeatherForecastDtoFactory
  {
    PersistentWeatherForecastDto CreateFrom(WeatherForecastDto forecastDto, Guid id);
  }

  public class PersistentWeatherForecastDtoFactory : IPersistentWeatherForecastDtoFactory
  {
    public PersistentWeatherForecastDto CreateFrom(WeatherForecastDto forecastDto, Guid id)
    {
      var persistentWeatherForecastDto = new PersistentWeatherForecastDto(
        id,
        forecastDto.TenantId,
        forecastDto.UserId,
        forecastDto.Date,
        forecastDto.TemperatureC,
        forecastDto.Summary);
      return persistentWeatherForecastDto;
    }
  }
}