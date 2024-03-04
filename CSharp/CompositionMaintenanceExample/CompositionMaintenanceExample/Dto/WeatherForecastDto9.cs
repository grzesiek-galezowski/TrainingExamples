namespace CompositionMaintenanceExample.Dto;

public record WeatherForecastDto9(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}