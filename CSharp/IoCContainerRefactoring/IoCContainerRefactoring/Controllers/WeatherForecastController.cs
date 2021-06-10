using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace IoCContainerRefactoring.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class WeatherForecastController : ControllerBase
  {
    private readonly IWeatherForecastDao _weatherForecastDao;
    private readonly ITechSupport _support;
    private readonly IWeatherCommandFactory _weatherCommandFactory;

    public WeatherForecastController(
      IWeatherForecastDao weatherForecastDao,
      IWeatherCommandFactory weatherCommandFactory,
      ITechSupport support)
    {
      _weatherForecastDao = weatherForecastDao;
      _support = support;
      _weatherCommandFactory = weatherCommandFactory;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
      var responseInProgress = new GetWeatherForecastResponseInProgress();
      var command = _weatherCommandFactory.CreateGetReportedWeatherCommand(id, responseInProgress, _weatherForecastDao);
      await command.Execute();
      return responseInProgress.ToActionResult();
    }

    [HttpGet("{tenantId}/{userId}")]
    public IActionResult GetAllUserForecasts(string tenantId, string userId)
    {
      var responseInProgress = new AllUserForecastsResponseInProgress();
      var command = _weatherCommandFactory
        .CreateGetAllUserForecastsCommand(tenantId, userId, responseInProgress, _weatherForecastDao);
      command.Execute();
      return responseInProgress.ToActionResult();
    }

    [HttpPost]
    public async Task<IActionResult> ReportWeather(WeatherForecastDto forecastDto)
    {
      _support.NotifyReportWeatherStarted(this);

      var responseInProgress = new ReportWeatherResponseInProgress();
      var reportWeatherCommand = _weatherCommandFactory
        .CreateReportWeatherCommand(forecastDto, responseInProgress, _weatherForecastDao);
      await reportWeatherCommand.Execute();
      return responseInProgress.ToActionResult();
    }
  }
}
