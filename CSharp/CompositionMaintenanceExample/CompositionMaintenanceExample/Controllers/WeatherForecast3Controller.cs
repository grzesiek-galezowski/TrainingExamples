using CompositionMaintenanceExample.Dependencies;
using CompositionMaintenanceExample.Dto;
using Microsoft.AspNetCore.Mvc;

namespace CompositionMaintenanceExample.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecast3Controller(
  ILogger<WeatherForecast3Controller> logger,
  List<WeatherForecastDto3> controllerState,
  A31 a31,
  A32 a32,
  A33 a33,
  A34 a34) : ControllerBase
{

  [HttpGet]
  public WeatherForecastDto3 Get()
  {
    logger.LogInformation("Get called");
    return controllerState.Last();
  }

  [HttpPost]
  public WeatherForecastDto3 Save(WeatherForecastDto3 dto)
  {
    logger.LogInformation("Post called");
    controllerState.Add(dto);
    return dto;
  }
}