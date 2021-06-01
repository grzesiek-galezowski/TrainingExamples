using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flurl.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace IoCContainerRefactoring.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class WeatherForecastController : ControllerBase
  {
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IFlurlClient _flurlClient;
    private readonly WeatherForecastDao _weatherForecastDao;

    public WeatherForecastController(
      WeatherForecastDbContext db,
      ILogger<WeatherForecastController> logger,
      IFlurlClient flurlClient)
    {
      _weatherForecastDao = new WeatherForecastDao(db);
      _logger = logger;
      _flurlClient = flurlClient;
    }

    [HttpGet("{id}")]
    public async Task<WeatherForecastDto> Get(Guid id)
    {
      var persistentWeatherForecastDto = await _weatherForecastDao.ForecastById(id);

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
      return _weatherForecastDao._db.WeatherForecasts
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
        return new BadRequestResult();
      }

      var persistentWeatherForecastDto = new PersistentWeatherForecastDto(
        Guid.NewGuid(),
        forecastDto.TenantId,
        forecastDto.UserId,
        forecastDto.Date,
        forecastDto.TemperatureC,
        forecastDto.Summary);

      var entityEntry = await _weatherForecastDao._db.WeatherForecasts.AddAsync(persistentWeatherForecastDto);
      await _weatherForecastDao._db.SaveChangesAsync();

      await _flurlClient.Request("notifications").PostJsonAsync(
        new WeatherForecastSuccessfullyReportedEventDto(
          forecastDto.TenantId,
          forecastDto.UserId,
          forecastDto.TemperatureC));

      return new OkObjectResult(new ForecastCreationResultDto(entityEntry.Entity.Id));
    }
  }
}
