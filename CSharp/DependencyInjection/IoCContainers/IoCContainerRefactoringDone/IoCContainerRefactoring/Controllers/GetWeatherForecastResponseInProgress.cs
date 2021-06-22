using System;
using Microsoft.AspNetCore.Mvc;

namespace IoCContainerRefactoring.Controllers
{
  public class GetWeatherForecastResponseInProgress
  {
    private IActionResult _result;

    public IActionResult ToActionResult()
    {
      return _result ?? throw new InvalidOperationException();
    }

    public void Success(WeatherForecastDto weatherForecastDto)
    {
      _result = new OkObjectResult(weatherForecastDto);
    }
  }
}