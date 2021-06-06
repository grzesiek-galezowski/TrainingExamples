using System.Threading.Tasks;

namespace IoCContainerRefactoring.Controllers
{
  public class ReportWeatherCommand : IWeatherCommand
  {
    private readonly ITechSupport _support;
    private readonly IEventPipe _eventPipe;
    private readonly IWeatherForecastDao _weatherForecastDao;
    private readonly IPersistentWeatherForecastDtoFactory _persistentWeatherForecastDtoFactory;
    private readonly IIdGenerator _idGenerator;
    private readonly ReportWeatherResponseInProgress _responseInProgress;
    private readonly WeatherForecastDto _forecastDto;

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
}