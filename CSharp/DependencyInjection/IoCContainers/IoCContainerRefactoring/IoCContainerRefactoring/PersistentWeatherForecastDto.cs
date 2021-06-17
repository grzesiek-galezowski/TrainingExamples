using System;

namespace IoCContainerRefactoring
{
  public record PersistentWeatherForecastDto(
    Guid Id,
    string TenantId,
    string UserId,
    DateTime Date,
    int TemperatureC,
    string Summary) {}
}