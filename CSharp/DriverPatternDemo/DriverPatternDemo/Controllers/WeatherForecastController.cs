using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace IoCContainerRefactoring.Controllers
{
  public class ReportWeatherResponseInProgress
  {
    private IActionResult _result;

    public IActionResult ToActionResult()
    {
      return _result ?? throw new InvalidOperationException();
    }

    public void FailedBecauseTemperatureIsTooLow()
    {
      _result = new BadRequestResult();
    }

    public void Success(ForecastCreationResultDto forecastCreationResultDto)
    {
      _result = new OkObjectResult(forecastCreationResultDto);
    }
  }

  public class ReportWeatherCommand
  {
    public ITechSupport _support;
    public IEventPipe _eventPipe;
    public IWeatherForecastDao _weatherForecastDao;
    public IPersistentWeatherForecastDtoFactory _persistentWeatherForecastDtoFactory;
    public IIdGenerator _idGenerator;
    public ReportWeatherResponseInProgress _responseInProgress;
    public WeatherForecastDto _forecastDto;

    public ReportWeatherCommand(WeatherForecastDto forecastDto, ReportWeatherResponseInProgress responseInProgress, IIdGenerator idGenerator, IPersistentWeatherForecastDtoFactory persistentWeatherForecastDtoFactory, IWeatherForecastDao weatherForecastDao, IEventPipe eventPipe, ITechSupport support)
    {
      _forecastDto = forecastDto;
      _responseInProgress = responseInProgress;
      _idGenerator = idGenerator;
      _persistentWeatherForecastDtoFactory = persistentWeatherForecastDtoFactory;
      _weatherForecastDao = weatherForecastDao;
      _eventPipe = eventPipe;
      _support = support;
    }

    public async Task Execute()
    {
      if (_forecastDto.TemperatureC < -100)
      {
        _support.NotifyTemperatureTooLowIn(new object(/* bug */), _forecastDto);
        _responseInProgress.FailedBecauseTemperatureIsTooLow();
      }
      else
      {
        var id = _idGenerator.NewId();
        var persistentWeatherForecastDto = _persistentWeatherForecastDtoFactory
          .CreateFrom(_forecastDto, id);

        await _weatherForecastDao.SaveAsync(persistentWeatherForecastDto);

        var eventDto = new WeatherForecastSuccessfullyReportedEventDto(
          _forecastDto.TenantId,
          _forecastDto.UserId,
          _forecastDto.TemperatureC);
        await _eventPipe.SendNotificationAsync(eventDto);
        _responseInProgress.Success(new ForecastCreationResultDto(id));
      }
    }
  }

  public class WeatherCommandFactory
  {
    public IIdGenerator _idGenerator;
    public IPersistentWeatherForecastDtoFactory _persistentWeatherForecastDtoFactory;
    public readonly IWeatherForecastDtoFactory _weatherForecastDtoFactory;
    public IWeatherForecastDao _weatherForecastDao;
    public IEventPipe _eventPipe;
    public ITechSupport _techSupport;

    public WeatherCommandFactory(ITechSupport techSupport, IEventPipe eventPipe, IWeatherForecastDao weatherForecastDao,
      IPersistentWeatherForecastDtoFactory persistentWeatherForecastDtoFactory,
      IWeatherForecastDtoFactory weatherForecastDtoFactory, IIdGenerator idGenerator)
    {
      _techSupport = techSupport;
      _eventPipe = eventPipe;
      _weatherForecastDao = weatherForecastDao;
      _persistentWeatherForecastDtoFactory = persistentWeatherForecastDtoFactory;
      _weatherForecastDtoFactory = weatherForecastDtoFactory;
      _idGenerator = idGenerator;
    }

    public ReportWeatherCommand CreateReportWeatherCommand(WeatherForecastDto forecastDto, ReportWeatherResponseInProgress responseInProgress)
    {
      return new ReportWeatherCommand(forecastDto, responseInProgress, _idGenerator, _persistentWeatherForecastDtoFactory, _weatherForecastDao, _eventPipe, _techSupport);
    }

    public GetReportedWeatherCommand CreateGetReportedWeatherCommand(Guid id, GetWeatherForecastResponseInProgress responseInProgress)
    {
      return new GetReportedWeatherCommand(
        _weatherForecastDtoFactory, _techSupport, _weatherForecastDao, id, responseInProgress);
    }
  }

  public class GetReportedWeatherCommand
  {
    public GetWeatherForecastResponseInProgress _responseInProgress;
    public Guid _id;
    public IWeatherForecastDao _weatherForecastDao;
    public ITechSupport _techSupport;
    public IWeatherForecastDtoFactory _weatherForecastDtoFactory;

    public GetReportedWeatherCommand(
      IWeatherForecastDtoFactory weatherForecastDtoFactory, 
      ITechSupport techSupport, 
      IWeatherForecastDao weatherForecastDao, 
      Guid id, 
      GetWeatherForecastResponseInProgress responseInProgress)
    {
      _weatherForecastDtoFactory = weatherForecastDtoFactory;
      _techSupport = techSupport;
      _weatherForecastDao = weatherForecastDao;
      _id = id;
      _responseInProgress = responseInProgress;
    }

    public async Task Execute()
    {
      var persistentWeatherForecastDto = await _weatherForecastDao.ForecastById(_id);
      _techSupport.NotifyLoaded(new object(/* bug */), persistentWeatherForecastDto);

      var weatherForecastDto = _weatherForecastDtoFactory.CreateFrom(persistentWeatherForecastDto);
      _responseInProgress.Success(weatherForecastDto);
    }
  }

  [ApiController]
  [Route("[controller]")]
  public class WeatherForecastController : ControllerBase
  {
    private readonly IWeatherForecastDao _weatherForecastDao;
    private readonly IWeatherForecastDtoFactory _weatherForecastDtoFactory;
    private readonly WeatherCommandFactory _weatherCommandFactory;

    public WeatherForecastController(
      IWeatherForecastDao weatherForecastDao, 
      IEventPipe eventPipe, 
      IIdGenerator idGenerator, 
      IPersistentWeatherForecastDtoFactory persistentWeatherForecastDtoFactory, 
      IWeatherForecastDtoFactory weatherForecastDtoFactory, 
      ITechSupport support)
    {
      _weatherForecastDao = weatherForecastDao;
      _weatherForecastDtoFactory = weatherForecastDtoFactory;
      _weatherCommandFactory = new WeatherCommandFactory(
        support, 
        eventPipe, 
        _weatherForecastDao, 
        persistentWeatherForecastDtoFactory, 
        weatherForecastDtoFactory,
        idGenerator);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
      var responseInProgress = new GetWeatherForecastResponseInProgress();
      var command = _weatherCommandFactory.CreateGetReportedWeatherCommand(id, responseInProgress);
      await command.Execute();
      return responseInProgress.ToActionResult();
    }

    [HttpGet("{tenantId}/{userId}")]
    public IActionResult GetAllUserForecasts(string tenantId, string userId)
    {
      var weatherForecastDtos = _weatherForecastDtoFactory.CreateFrom(
        _weatherForecastDao.ForecastsOf(userId, tenantId));
      return new OkObjectResult(weatherForecastDtos);
    }

    [HttpPost]
    public async Task<IActionResult> ReportWeather(WeatherForecastDto forecastDto)
    {
      var responseInProgress = new ReportWeatherResponseInProgress();
      var reportWeatherCommand = _weatherCommandFactory.CreateReportWeatherCommand(forecastDto, responseInProgress);
      await reportWeatherCommand.Execute();
      return responseInProgress.ToActionResult();
    }
  }
}
