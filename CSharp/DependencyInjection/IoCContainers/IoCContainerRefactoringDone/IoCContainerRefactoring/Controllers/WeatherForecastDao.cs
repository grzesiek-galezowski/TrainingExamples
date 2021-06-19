using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace IoCContainerRefactoring.Controllers
{
  public interface IWeatherForecastDao
  {
    Task<PersistentWeatherForecastDto> ForecastById(Guid id);
    IQueryable<PersistentWeatherForecastDto> ForecastsOf(string userId, string tenantId);
    Task SaveAsync(PersistentWeatherForecastDto persistentWeatherForecastDto);
  }

  public class WeatherForecastDao : IWeatherForecastDao
  {
    private readonly WeatherForecastDbContext _db;

    public WeatherForecastDao(WeatherForecastDbContext db)
    {
      _db = db;
    }

    public Task<PersistentWeatherForecastDto> ForecastById(Guid id)
    {
      return _db.WeatherForecasts.SingleAsync(dto => dto.Id == id);
    }

    public IQueryable<PersistentWeatherForecastDto> ForecastsOf(string userId, string tenantId)
    {
      return _db.WeatherForecasts
        .Where(f => f.TenantId == tenantId && f.UserId == userId);
    }

    public async Task SaveAsync(PersistentWeatherForecastDto persistentWeatherForecastDto)
    {
      await _db.WeatherForecasts.AddAsync(persistentWeatherForecastDto);
      await _db.SaveChangesAsync();
    }
  }
}