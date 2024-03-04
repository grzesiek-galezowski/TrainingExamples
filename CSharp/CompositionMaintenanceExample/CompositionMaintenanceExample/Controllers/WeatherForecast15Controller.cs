using CompositionMaintenanceExample.Dependencies;
using CompositionMaintenanceExample.Dto;
using Microsoft.AspNetCore.Mvc;

namespace CompositionMaintenanceExample.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecast15Controller(
  ILogger<WeatherForecast15Controller> logger,
  List<WeatherForecastDto15> controllerState,
  A151 a151,
  A152 a152,
  A153 a153,
  A154 a154) : ControllerBase
{
  [HttpGet]
  public WeatherForecastDto15 Get()
  {
    logger.LogInformation("Get called");
    return controllerState.Last();
  }

  [HttpPost]
  public WeatherForecastDto15 Save(WeatherForecastDto15 dto)
  {
    logger.LogInformation("Post called");
    controllerState.Add(dto);
    return dto;
  }
}