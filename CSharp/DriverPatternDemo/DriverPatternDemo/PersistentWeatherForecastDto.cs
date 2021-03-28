using System;

namespace DriverPatternDemo
{
  public record PersistentWeatherForecastDto(
    Guid Id, 
    DateTime Date, 
    int TemperatureC, 
    string Summary) {}
}