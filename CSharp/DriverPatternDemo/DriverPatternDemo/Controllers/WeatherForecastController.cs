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
  public interface IEventPipe
  {
    Task<IFlurlResponse> SendNotificationAsync(WeatherForecastSuccessfullyReportedEventDto eventDto);
  }

  public class EventPipe : IEventPipe
  {
    private readonly IFlurlClient _flurlClient;

    public EventPipe(IFlurlClient flurlClient)
    {
      _flurlClient = flurlClient;
    }

    public Task<IFlurlResponse> SendNotificationAsync(WeatherForecastSuccessfullyReportedEventDto eventDto)
    {
      return _flurlClient.Request("notifications").PostJsonAsync(eventDto);
    }
  }

  [ApiController]
  [Route("[controller]")]
  public class WeatherForecastController : ControllerBase
  {
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IWeatherForecastDao _weatherForecastDao;
    private readonly IEventPipe _eventPipe;
    private readonly WeatherForecastDtoFactory _weatherForecastDtoFactory;

    public WeatherForecastController(
      ILogger<WeatherForecastController> logger, 
      IWeatherForecastDao weatherForecastDao, 
      IEventPipe eventPipe)
    {
      _weatherForecastDao = weatherForecastDao;
      _logger = logger;
      _eventPipe = eventPipe;
      _weatherForecastDtoFactory = new WeatherForecastDtoFactory();
    }

    [HttpGet("{id}")]
    public async Task<WeatherForecastDto> Get(Guid id)
    {
      var persistentWeatherForecastDto = await _weatherForecastDao.ForecastById(id);

      return _weatherForecastDtoFactory.CreateFrom(persistentWeatherForecastDto);
    }

    [HttpGet("{tenantId}/{userId}")]
    public IEnumerable<WeatherForecastDto> GetAllUserForecasts(string tenantId, string userId)
    {
      return _weatherForecastDtoFactory.CreateFrom(
        _weatherForecastDao.ForecastsOf(userId, tenantId));
    }

    [HttpPost]
    public async Task<ActionResult> ReportWeather(WeatherForecastDto forecastDto)
    {
      if (forecastDto.TemperatureC < -100)
      {
        return new BadRequestResult();
      }

      var id = Guid.NewGuid();
      var persistentWeatherForecastDto = new PersistentWeatherForecastDto(
        id,
        forecastDto.TenantId,
        forecastDto.UserId,
        forecastDto.Date,
        forecastDto.TemperatureC,
        forecastDto.Summary);

      await _weatherForecastDao.SaveAsync(persistentWeatherForecastDto);

      var eventDto = new WeatherForecastSuccessfullyReportedEventDto(
        forecastDto.TenantId,
        forecastDto.UserId,
        forecastDto.TemperatureC);
      await _eventPipe.SendNotificationAsync(eventDto);

      return new OkObjectResult(new ForecastCreationResultDto(id));
    }
  }
}
