using System.Threading.Tasks;

namespace IoCContainerRefactoring.Controllers
{
  public class GetAllUserForecastsCommand : IWeatherCommand
  {
    private readonly AllUserForecastsResponseInProgress _responseInProgress;
    private readonly string _userId;
    private readonly string _tenantId;
    private readonly IWeatherForecastDtoFactory _weatherForecastDtoFactory;
    private readonly IWeatherForecastDao _weatherForecastDao;

    public GetAllUserForecastsCommand(
      IWeatherForecastDao weatherForecastDao, 
      IWeatherForecastDtoFactory weatherForecastDtoFactory, 
      string tenantId, 
      string userId, 
      AllUserForecastsResponseInProgress responseInProgress)
    {
      _weatherForecastDao = weatherForecastDao;
      _weatherForecastDtoFactory = weatherForecastDtoFactory;
      _tenantId = tenantId;
      _userId = userId;
      _responseInProgress = responseInProgress;
    }

    public Task Execute()
    {
      var weatherForecastDtos = _weatherForecastDtoFactory.CreateFrom(
        _weatherForecastDao.ForecastsOf(_userId, _tenantId));
      _responseInProgress.Success(weatherForecastDtos);
      return Task.CompletedTask;
    }
  }
}