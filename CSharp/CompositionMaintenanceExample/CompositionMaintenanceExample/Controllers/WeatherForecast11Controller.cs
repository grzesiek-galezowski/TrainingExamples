using CompositionMaintenanceExample.Dependencies;
using CompositionMaintenanceExample.Dto;
using Microsoft.AspNetCore.Mvc;

namespace CompositionMaintenanceExample.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecast11Controller(
  ILogger<WeatherForecast11Controller> logger,
  List<WeatherForecastDto11> controllerState,
  A111 a111,
  A112 a112,
  A113 a113,
  A114 a114) : ControllerBase
{
  [HttpGet]
  public WeatherForecastDto11 Get()
  {
    logger.LogInformation("Get called");
    return controllerState.Last();
  }

  [HttpPost]
  public WeatherForecastDto11 Save(WeatherForecastDto11 dto)
  {
    logger.LogInformation("Post called");
    controllerState.Add(dto);
    return dto;
  }
}