using CompositionMaintenanceExample.Dependencies;
using CompositionMaintenanceExample.Dto;
using Microsoft.AspNetCore.Mvc;

namespace CompositionMaintenanceExample.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecast16Controller(
  ILogger<WeatherForecast16Controller> logger,
  List<WeatherForecastDto16> controllerState,
  A161 a161,
  A162 a162,
  A163 a163,
  A164 a164) : ControllerBase
{
  [HttpGet]
  public WeatherForecastDto16 Get()
  {
    logger.LogInformation("Get called");
    return controllerState.Last();
  }

  [HttpPost]
  public WeatherForecastDto16 Save(WeatherForecastDto16 dto)
  {
    logger.LogInformation("Post called");
    controllerState.Add(dto);
    return dto;
  }
}