using CompositionMaintenanceExample.Dependencies;
using CompositionMaintenanceExample.Dto;
using Microsoft.AspNetCore.Mvc;

namespace CompositionMaintenanceExample.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecast12Controller(
  ILogger<WeatherForecast12Controller> logger,
  List<WeatherForecastDto12> controllerState,
  A121 a121,
  A122 a122,
  A123 a123,
  A124 a124) : ControllerBase
{
  [HttpGet]
  public WeatherForecastDto12 Get()
  {
    logger.LogInformation("Get called");
    return controllerState.Last();
  }

  [HttpPost]
  public WeatherForecastDto12 Save(WeatherForecastDto12 dto)
  {
    logger.LogInformation("Post called");
    controllerState.Add(dto);
    return dto;
  }
}