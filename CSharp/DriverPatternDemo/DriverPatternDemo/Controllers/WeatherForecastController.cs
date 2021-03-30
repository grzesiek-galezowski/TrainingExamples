using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flurl.Http;
using Microsoft.EntityFrameworkCore;

namespace DriverPatternDemo.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class WeatherForecastController : ControllerBase
  {
    private readonly WeatherForecastDbContext _db;
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IFlurlClient _flurlClient;

    public WeatherForecastController(
      WeatherForecastDbContext db,
      ILogger<WeatherForecastController> logger,
      IFlurlClient flurlClient)
    {
      _db = db;
      _logger = logger;
      _flurlClient = flurlClient;
    }

    [HttpGet("{id}")]
    public async Task<WeatherForecastDto> Get(Guid id)
    {
      var persistentWeatherForecastDto = await _db.WeatherForecasts.SingleAsync(dto => dto.Id == id);

      return new WeatherForecastDto(
        persistentWeatherForecastDto.TenantId,
        persistentWeatherForecastDto.UserId,
        persistentWeatherForecastDto.Date, 
        persistentWeatherForecastDto.TemperatureC, 
        persistentWeatherForecastDto.Summary);
    }

    [HttpGet("{tenantId}/{userId}")]
    public IEnumerable<WeatherForecastDto> GetAllUserForecasts(string tenantId, string userId)
    {
      return _db.WeatherForecasts
        .Where(f => f.TenantId == tenantId && f.UserId == userId)
        .Select(f => new WeatherForecastDto(
          f.TenantId,
          f.UserId,
          f.Date,
          f.TemperatureC,
          f.Summary));
    }

    [HttpPost]
    public async Task<ActionResult> ReportWeather(WeatherForecastDto forecastDto)
    {
      if (forecastDto.TemperatureC < -100)
      {
        return BadRequest();
      }

      var persistentWeatherForecastDto = new PersistentWeatherForecastDto(
        Guid.NewGuid(),
        forecastDto.TenantId,
        forecastDto.UserId,
        forecastDto.Date,
        forecastDto.TemperatureC,
        forecastDto.Summary);

      var entityEntry = await _db.WeatherForecasts.AddAsync(persistentWeatherForecastDto);
      await _db.SaveChangesAsync();

      await _flurlClient.Request("notifications").PostJsonAsync(
        new WeatherForecastSuccessfullyReportedEventDto(
          forecastDto.TenantId,
          forecastDto.UserId,
          forecastDto.TemperatureC));

      return Ok(new ForecastCreationResultDto(entityEntry.Entity.Id));
    }
  }
}
