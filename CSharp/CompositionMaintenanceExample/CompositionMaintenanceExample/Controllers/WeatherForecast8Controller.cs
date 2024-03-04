using CompositionMaintenanceExample.Dependencies;
using CompositionMaintenanceExample.Dto;
using Microsoft.AspNetCore.Mvc;

namespace CompositionMaintenanceExample.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecast8Controller(
  ILogger<WeatherForecast8Controller> logger,
  List<WeatherForecastDto8> controllerState,
  A81 a81,
  A82 a82,
  A83 a83,
  A84 a84) : ControllerBase
{
  [HttpGet]
  public WeatherForecastDto8 Get()
  {
    logger.LogInformation("Get called");
    return controllerState.Last();
  }

  [HttpPost]
  public WeatherForecastDto8 Save(WeatherForecastDto8 dto)
  {
    logger.LogInformation("Post called");
    controllerState.Add(dto);
    return dto;
  }
}