using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DriverPatternDemo.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class WeatherForecastController : ControllerBase
  {
    private readonly WeatherForecastDbContext _db;
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(
      WeatherForecastDbContext db,
      ILogger<WeatherForecastController> logger)
    {
      _db = db;
      _logger = logger;
    }

    [HttpGet("{id}")]
    public async Task<WeatherForecastDto> Get(Guid id)
    {
      var persistentWeatherForecastDto = await _db.WeatherForecasts.SingleAsync(dto => dto.Id == id);

      return new WeatherForecastDto(
        persistentWeatherForecastDto.Date, 
        persistentWeatherForecastDto.TemperatureC, 
        persistentWeatherForecastDto.Summary);
    }

    [HttpGet]
    public IEnumerable<WeatherForecastDto> GetAll()
    {
      return _db.WeatherForecasts.Select(f => new WeatherForecastDto(f.Date, f.TemperatureC, f.Summary));
    }

    [HttpPost]
    public async Task<ActionResult> ReportWeather(WeatherForecastDto forecastDto)
    {
      var persistentWeatherForecastDto = new PersistentWeatherForecastDto(
        Guid.NewGuid(),
        forecastDto.Date,
        forecastDto.TemperatureC,
        forecastDto.Summary);
      var entityEntry = await _db.WeatherForecasts.AddAsync(persistentWeatherForecastDto);
      await _db.SaveChangesAsync();
      return Ok(new ForecastCreationResultDto(entityEntry.Entity.Id));
    }
  }
}
