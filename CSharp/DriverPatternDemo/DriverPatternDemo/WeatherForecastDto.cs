using System;

namespace DriverPatternDemo
{
  public record WeatherForecastDto(DateTime Date, int TemperatureC, string Summary)
  {
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
  }
}
