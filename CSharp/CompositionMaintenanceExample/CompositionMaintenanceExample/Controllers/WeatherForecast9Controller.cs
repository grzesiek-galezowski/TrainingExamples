using CompositionMaintenanceExample.Dependencies;
using CompositionMaintenanceExample.Dto;
using Microsoft.AspNetCore.Mvc;

namespace CompositionMaintenanceExample.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecast9Controller(
  ILogger<WeatherForecast9Controller> logger,
  List<WeatherForecastDto9> controllerState,
  A91 a91,
  A92 a92,
  A93 a93,
  A94 a94) : ControllerBase
{

  [HttpGet]
  public WeatherForecastDto9 Get()
  {
    logger.LogInformation("Get called");
    return controllerState.Last();
  }

  [HttpPost]
  public WeatherForecastDto9 Save(WeatherForecastDto9 dto)
  {
    logger.LogInformation("Post called");
    controllerState.Add(dto);
    return dto;
  }
}