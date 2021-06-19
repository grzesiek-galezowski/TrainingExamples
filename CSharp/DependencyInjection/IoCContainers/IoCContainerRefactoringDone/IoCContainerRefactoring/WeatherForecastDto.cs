using System;

namespace IoCContainerRefactoring
{
  public record WeatherForecastDto(string TenantId, string UserId, DateTime Date, int TemperatureC, string Summary)
  {
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
  }
}
