using CompositionMaintenanceExample.Dependencies;
using CompositionMaintenanceExample.Dto;
using Microsoft.AspNetCore.Mvc;

namespace CompositionMaintenanceExample.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecast7Controller(
  ILogger<WeatherForecast7Controller> logger,
  List<WeatherForecastDto7> controllerState,
  A71 a71,
  A72 a72,
  A73 a73,
  A74 a74) : ControllerBase
{
  [HttpGet]
  public WeatherForecastDto7 Get()
  {
    logger.LogInformation("Get called");
    return controllerState.Last();
  }

  [HttpPost]
  public WeatherForecastDto7 Save(WeatherForecastDto7 dto)
  {
    logger.LogInformation("Post called");
    controllerState.Add(dto);
    return dto;
  }
}