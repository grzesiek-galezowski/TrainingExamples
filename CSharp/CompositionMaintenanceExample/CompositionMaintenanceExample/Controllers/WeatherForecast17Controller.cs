using CompositionMaintenanceExample.Dependencies;
using CompositionMaintenanceExample.Dto;
using Microsoft.AspNetCore.Mvc;

namespace CompositionMaintenanceExample.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecast17Controller(
  ILogger<WeatherForecast17Controller> logger,
  List<WeatherForecastDto17> controllerState,
  A171 a171,
  A172 a172,
  A173 a173,
  A174 a174) : ControllerBase
{
  [HttpGet]
  public WeatherForecastDto17 Get()
  {
    logger.LogInformation("Get called");
    return controllerState.Last();
  }

  [HttpPost]
  public WeatherForecastDto17 Save(WeatherForecastDto17 dto)
  {
    logger.LogInformation("Post called");
    controllerState.Add(dto);
    return dto;
  }
}