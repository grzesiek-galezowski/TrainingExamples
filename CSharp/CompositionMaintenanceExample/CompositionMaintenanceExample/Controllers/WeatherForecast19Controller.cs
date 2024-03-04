using CompositionMaintenanceExample.Dependencies;
using CompositionMaintenanceExample.Dto;
using Microsoft.AspNetCore.Mvc;

namespace CompositionMaintenanceExample.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecast19Controller(
  ILogger<WeatherForecast19Controller> logger,
  List<WeatherForecastDto19> controllerState,
  A191 a191,
  A192 a192,
  A193 a193,
  A194 a194) : ControllerBase
{
  [HttpGet]
  public WeatherForecastDto19 Get()
  {
    logger.LogInformation("Get called");
    return controllerState.Last();
  }

  [HttpPost]
  public WeatherForecastDto19 Save(WeatherForecastDto19 dto)
  {
    logger.LogInformation("Post called");
    controllerState.Add(dto);
    return dto;
  }
}