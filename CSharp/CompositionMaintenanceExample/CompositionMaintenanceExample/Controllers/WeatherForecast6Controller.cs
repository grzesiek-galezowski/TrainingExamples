using CompositionMaintenanceExample.Dependencies;
using CompositionMaintenanceExample.Dto;
using Microsoft.AspNetCore.Mvc;

namespace CompositionMaintenanceExample.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecast6Controller(
  ILogger<WeatherForecast6Controller> logger,
  List<WeatherForecastDto6> controllerState,
  A61 a61,
  A62 a62,
  A63 a63,
  A64 a64) : ControllerBase
{
  [HttpGet]
  public WeatherForecastDto6 Get()
  {
    logger.LogInformation("Get called");
    return controllerState.Last();
  }

  [HttpPost]
  public WeatherForecastDto6 Save(WeatherForecastDto6 dto)
  {
    logger.LogInformation("Post called");
    controllerState.Add(dto);
    return dto;
  }
}