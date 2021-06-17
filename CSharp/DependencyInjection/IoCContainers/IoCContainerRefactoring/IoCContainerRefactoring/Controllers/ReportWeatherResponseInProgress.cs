using System;
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
}