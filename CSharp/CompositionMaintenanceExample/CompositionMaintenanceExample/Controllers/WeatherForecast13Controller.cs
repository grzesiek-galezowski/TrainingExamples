using CompositionMaintenanceExample.Dependencies;
using CompositionMaintenanceExample.Dto;
using Microsoft.AspNetCore.Mvc;

namespace CompositionMaintenanceExample.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecast13Controller(
  ILogger<WeatherForecast13Controller> logger,
  List<WeatherForecastDto13> controllerState,
  A131 a131,
  A132 a132,
  A133 a133,
  A134 a134) : ControllerBase
{
  [HttpGet]
  public WeatherForecastDto13 Get()
  {
    logger.LogInformation("Get called");
    return controllerState.Last();
  }

  [HttpPost]
  public WeatherForecastDto13 Save(WeatherForecastDto13 dto)
  {
    logger.LogInformation("Post called");
    controllerState.Add(dto);
    return dto;
  }
}