using CompositionMaintenanceExample.Dependencies;
using CompositionMaintenanceExample.Dto;
using Microsoft.AspNetCore.Mvc;

namespace CompositionMaintenanceExample.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecast14Controller(
  ILogger<WeatherForecast14Controller> logger,
  List<WeatherForecastDto14> controllerState,
  A141 a141,
  A142 a142,
  A143 a143,
  A144 a144) : ControllerBase
{
  [HttpGet]
  public WeatherForecastDto14 Get()
  {
    logger.LogInformation("Get called");
    return controllerState.Last();
  }

  [HttpPost]
  public WeatherForecastDto14 Save(WeatherForecastDto14 dto)
  {
    logger.LogInformation("Post called");
    controllerState.Add(dto);
    return dto;
  }
}