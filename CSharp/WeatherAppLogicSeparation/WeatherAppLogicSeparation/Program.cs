
using Serilog;
using WeatherAppLogicSeparation.Controllers;

namespace WeatherAppLogicSeparation;

public class Program
{
  public static void Main(string[] args)
  {
    var logger = new LoggerConfiguration().CreateLogger();
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddSingleton(_ => new ServiceLogic(logger));
    builder.Services.AddTransient<WeatherForecastController>(ctx =>
      ctx.GetRequiredService<ServiceLogic>().CreateWeatherController());
    // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
    builder.Services.AddOpenApi();
    builder.Services.AddHealthChecks();
    builder.Logging.AddSerilog(logger);
    builder.Services.AddControllers().AddControllersAsServices();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
      app.MapOpenApi();
    }

    app.UseHttpsRedirection();
    app.MapHealthChecks("/healthy");
    app.UseAuthorization();
    app.MapControllers();

    app.Run();
  }
}