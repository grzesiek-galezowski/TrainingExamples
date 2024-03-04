namespace CompositionMaintenanceExample.Dto;

public record WeatherForecastDto6(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}