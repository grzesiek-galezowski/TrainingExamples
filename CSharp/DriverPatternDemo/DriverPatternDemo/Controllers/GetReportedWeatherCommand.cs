using System;
using System.Threading.Tasks;

namespace IoCContainerRefactoring.Controllers
{
  public class GetReportedWeatherCommand : IWeatherCommand
  {
    private readonly GetWeatherForecastResponseInProgress _responseInProgress;
    private readonly Guid _id;
    private readonly IWeatherForecastDao _weatherForecastDao;
    private readonly ITechSupport _techSupport;
    private readonly IWeatherForecastDtoFactory _weatherForecastDtoFactory;

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
}