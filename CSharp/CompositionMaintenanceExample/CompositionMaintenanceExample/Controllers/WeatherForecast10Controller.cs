using CompositionMaintenanceExample.Dependencies;
using CompositionMaintenanceExample.Dto;
using Microsoft.AspNetCore.Mvc;

namespace CompositionMaintenanceExample.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecast10Controller(
  ILogger<WeatherForecast10Controller> logger,
  List<WeatherForecastDto10> controllerState,
  A101 a101,
  A102 a102,
  A103 a103,
  A104 a104) : ControllerBase
{
  [HttpGet]
  public WeatherForecastDto10 Get()
  {
    logger.LogInformation("Get called");
    return controllerState.Last();
  }

  [HttpPost]
  public WeatherForecastDto10 Save(WeatherForecastDto10 dto)
  {
    logger.LogInformation("Post called");
    controllerState.Add(dto);
    return dto;
  }
}