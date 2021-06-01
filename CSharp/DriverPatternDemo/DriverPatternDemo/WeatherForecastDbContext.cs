using Microsoft.EntityFrameworkCore;

namespace IoCContainerRefactoring
{
  public class WeatherForecastDbContext : DbContext
  {
    public WeatherForecastDbContext(DbContextOptions<WeatherForecastDbContext> options)
      : base(options) { }

    public DbSet<PersistentWeatherForecastDto> WeatherForecasts { get; set; }
  }
}