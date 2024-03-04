using CompositionMaintenanceExample.Dependencies;
using CompositionMaintenanceExample.Dto;
using Microsoft.AspNetCore.Mvc;

namespace CompositionMaintenanceExample.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecast2Controller(
  ILogger<WeatherForecast2Controller> logger,
  List<WeatherForecastDto2> controllerState,
  A21 a21,
  A22 a22,
  A23 a23,
  A24 a24) : ControllerBase
{

  [HttpGet]
  public WeatherForecastDto2 Get()
  {
    logger.LogInformation("Get called");
    return controllerState.Last();
  }

  [HttpPost]
  public WeatherForecastDto2 Save(WeatherForecastDto2 dto)
  {
    logger.LogInformation("Post called");
    controllerState.Add(dto);
    return dto;
  }
}