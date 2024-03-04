using CompositionMaintenanceExample.Dependencies;
using CompositionMaintenanceExample.Dto;
using Microsoft.AspNetCore.Mvc;

namespace CompositionMaintenanceExample.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecast5Controller(
  ILogger<WeatherForecast5Controller> logger,
  List<WeatherForecastDto5> controllerState,
  A51 a51,
  A52 a52,
  A53 a53,
  A54 a54) : ControllerBase
{
  [HttpGet]
  public WeatherForecastDto5 Get()
  {
    logger.LogInformation("Get called");
    return controllerState.Last();
  }

  [HttpPost]
  public WeatherForecastDto5 Save(WeatherForecastDto5 dto)
  {
    logger.LogInformation("Post called");
    controllerState.Add(dto);
    return dto;
  }
}