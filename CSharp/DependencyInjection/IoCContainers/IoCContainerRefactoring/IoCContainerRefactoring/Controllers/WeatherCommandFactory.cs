using System;

namespace IoCContainerRefactoring.Controllers
{
  public interface IWeatherCommandFactory
  {
    IWeatherCommand CreateReportWeatherCommand(WeatherForecastDto forecastDto,
      ReportWeatherResponseInProgress responseInProgress, IWeatherForecastDao weatherForecastDao);
    IWeatherCommand CreateGetReportedWeatherCommand(Guid id, GetWeatherForecastResponseInProgress responseInProgress,
      IWeatherForecastDao weatherForecastDao);
    IWeatherCommand CreateGetAllUserForecastsCommand(string tenantId, string userId,
      AllUserForecastsResponseInProgress responseInProgress, IWeatherForecastDao weatherForecastDao);
  }

  public class WeatherCommandFactory : IWeatherCommandFactory
  {
    private readonly IIdGenerator _idGenerator;
    private readonly IPersistentWeatherForecastDtoFactory _persistentWeatherForecastDtoFactory;
    private readonly IWeatherForecastDtoFactory _weatherForecastDtoFactory;
    private readonly IEventPipe _eventPipe;
    private readonly ITechSupport _techSupport;

    public WeatherCommandFactory(
      ITechSupport techSupport, 
      IEventPipe eventPipe,
      IPersistentWeatherForecastDtoFactory persistentWeatherForecastDtoFactory,
      IWeatherForecastDtoFactory weatherForecastDtoFactory, 
      IIdGenerator idGenerator)
    {
      _techSupport = techSupport;
      _eventPipe = eventPipe;
      _persistentWeatherForecastDtoFactory = persistentWeatherForecastDtoFactory;
      _weatherForecastDtoFactory = weatherForecastDtoFactory;
      _idGenerator = idGenerator;
    }

    public IWeatherCommand CreateReportWeatherCommand(WeatherForecastDto forecastDto,
      ReportWeatherResponseInProgress responseInProgress, IWeatherForecastDao weatherForecastDao)
    {
      return new ReportWeatherCommand(forecastDto, responseInProgress, _idGenerator, _persistentWeatherForecastDtoFactory, weatherForecastDao, _eventPipe, _techSupport);
    }

    public IWeatherCommand CreateGetReportedWeatherCommand(Guid id,
      GetWeatherForecastResponseInProgress responseInProgress, IWeatherForecastDao weatherForecastDao)
    {
      return new GetReportedWeatherCommand(
        _weatherForecastDtoFactory, _techSupport, weatherForecastDao, id, responseInProgress);
    }

    public IWeatherCommand CreateGetAllUserForecastsCommand(string tenantId, string userId,
      AllUserForecastsResponseInProgress responseInProgress, IWeatherForecastDao weatherForecastDao)
    {
      return new GetAllUserForecastsCommand(weatherForecastDao, _weatherForecastDtoFactory, tenantId, userId, responseInProgress);
    }
  }
}