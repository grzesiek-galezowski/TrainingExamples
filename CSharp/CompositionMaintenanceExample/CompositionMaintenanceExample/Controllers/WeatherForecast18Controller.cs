using CompositionMaintenanceExample.Dependencies;
using CompositionMaintenanceExample.Dto;
using Microsoft.AspNetCore.Mvc;

namespace CompositionMaintenanceExample.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecast18Controller(
  ILogger<WeatherForecast18Controller> logger,
  List<WeatherForecastDto18> controllerState,
  A181 a181,
  A182 a182,
  A183 a183,
  A184 a184) : ControllerBase
{
  [HttpGet]
  public WeatherForecastDto18 Get()
  {
    logger.LogInformation("Get called");
    return controllerState.Last();
  }

  [HttpPost]
  public WeatherForecastDto18 Save(WeatherForecastDto18 dto)
  {
    logger.LogInformation("Post called");
    controllerState.Add(dto);
    return dto;
  }
}