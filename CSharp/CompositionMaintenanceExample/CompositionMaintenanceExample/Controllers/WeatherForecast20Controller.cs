using CompositionMaintenanceExample.Dependencies;
using CompositionMaintenanceExample.Dto;
using Microsoft.AspNetCore.Mvc;

namespace CompositionMaintenanceExample.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecast20Controller(
  ILogger<WeatherForecast20Controller> logger,
  List<WeatherForecastDto20> controllerState,
  A201 a201,
  A202 a202,
  A203 a203,
  A204 a204) : ControllerBase
{
  [HttpGet]
  public WeatherForecastDto20 Get()
  {
    logger.LogInformation("Get called");
    return controllerState.Last();
  }

  [HttpPost]
  public WeatherForecastDto20 Save(WeatherForecastDto20 dto)
  {
    logger.LogInformation("Post called");
    controllerState.Add(dto);
    return dto;
  }
}