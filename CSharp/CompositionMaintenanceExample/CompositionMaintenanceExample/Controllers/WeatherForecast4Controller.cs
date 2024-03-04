using CompositionMaintenanceExample.Dependencies;
using CompositionMaintenanceExample.Dto;
using Microsoft.AspNetCore.Mvc;

namespace CompositionMaintenanceExample.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecast4Controller(
  ILogger<WeatherForecast4Controller> logger,
  List<WeatherForecastDto4> controllerState,
  A41 a41,
  A42 a42,
  A43 a43,
  A44 a44) : ControllerBase
{

  [HttpGet]
  public WeatherForecastDto4 Get()
  {
    logger.LogInformation("Get called");
    return controllerState.Last();
  }

  [HttpPost]
  public WeatherForecastDto4 Save(WeatherForecastDto4 dto)
  {
    logger.LogInformation("Post called");
    controllerState.Add(dto);
    return dto;
  }
}