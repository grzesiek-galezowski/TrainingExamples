using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace IoCContainerRefactoring.Controllers
{
  public class AllUserForecastsResponseInProgress
  {
    private IActionResult _response;

    public IActionResult ToActionResult()
    {
      return _response;
    }

    public void Success(IEnumerable<WeatherForecastDto> weatherForecastDtos)
    { 
      _response = new OkObjectResult(weatherForecastDtos);
    }
  }
}