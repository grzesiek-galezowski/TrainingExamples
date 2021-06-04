using System;

namespace IoCContainerRefactoring.Controllers
{
  internal class PersistentWeatherForecastDtoFactory
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