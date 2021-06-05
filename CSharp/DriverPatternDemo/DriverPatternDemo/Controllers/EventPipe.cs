using System.Threading.Tasks;
using Flurl.Http;

namespace IoCContainerRefactoring.Controllers
{
  public interface IEventPipe
  {
    Task<IFlurlResponse> SendNotificationAsync(
      WeatherForecastSuccessfullyReportedEventDto eventDto);
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
}