using System.Collections.Generic;
using System.Linq;

namespace IoCContainerRefactoring.Controllers
{
  public interface IWeatherForecastDtoFactory
  {
    WeatherForecastDto CreateFrom(PersistentWeatherForecastDto persistentWeatherForecastDto);
    IEnumerable<WeatherForecastDto> CreateFrom(IEnumerable<PersistentWeatherForecastDto> persistentWeatherForecastDtos);
  }

  public class WeatherForecastDtoFactory : IWeatherForecastDtoFactory
  {
    public WeatherForecastDto CreateFrom(PersistentWeatherForecastDto persistentWeatherForecastDto)
    {
      return new WeatherForecastDto(
        persistentWeatherForecastDto.TenantId,
        persistentWeatherForecastDto.UserId,
        persistentWeatherForecastDto.Date, 
        persistentWeatherForecastDto.TemperatureC, 
        persistentWeatherForecastDto.Summary);
    }

    public IEnumerable<WeatherForecastDto> CreateFrom(IEnumerable<PersistentWeatherForecastDto> persistentWeatherForecastDtos)
    {
      return persistentWeatherForecastDtos.Select(CreateFrom);
    }
  }
}