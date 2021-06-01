using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace IoCContainerRefactoring.Controllers
{
  public class WeatherForecastDao
  {
    public readonly WeatherForecastDbContext _db;

    public WeatherForecastDao(WeatherForecastDbContext db)
    {
      _db = db;
    }

    public Task<PersistentWeatherForecastDto> ForecastById(Guid id)
    {
      return _db.WeatherForecasts.SingleAsync(dto => dto.Id == id);
    }
  }
}