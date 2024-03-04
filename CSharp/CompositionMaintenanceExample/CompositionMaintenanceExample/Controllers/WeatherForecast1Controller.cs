using CompositionMaintenanceExample.Dependencies;
using CompositionMaintenanceExample.Dto;
using Microsoft.AspNetCore.Mvc;

namespace CompositionMaintenanceExample.Controllers
{
    [ApiController]
  [Route("[controller]")]
  public class WeatherForecast1Controller(
    ILogger<WeatherForecast1Controller> logger,
    ICollection<WeatherForecastDto1> controllerState,
    A11 a11,
    A12 a12,
    A13 a13,
    A14 a14) : ControllerBase
  {
    [HttpGet]
    public WeatherForecastDto1 Get()
    {
      logger.LogInformation("Get called");
      return controllerState.Last();
    }

    [HttpPost]
    public WeatherForecastDto1 Save(WeatherForecastDto1 dto)
    {
      logger.LogInformation("Post called");
      controllerState.Add(dto);
      return dto;
    }
  }
}