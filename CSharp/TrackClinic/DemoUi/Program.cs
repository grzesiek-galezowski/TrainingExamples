using DemoUi;
using DemoUi.Adapters.Configuration;
using DemoUi.Components;
using DemoUi.Logic;
using Microsoft.Extensions.ServiceDiscovery.Http;
using Serilog;
using ServiceBuildingBlocks;

public partial class Program
{
  public static void Main(string[] args)
  {
    Log.Logger = LoggingConfigurations.ConfigureBootstrapLogger(new LoggerConfiguration());

    try
    {
      Log.Information("Starting application");
      var builder = WebApplication.CreateBuilder(args);
      var appConfig = AppConfig.From(builder.Configuration);

      builder.Host.UseSerilog((_, loggerConfiguration) =>
        LoggingConfigurations.ConfigureServiceLogging(
          loggerConfiguration,
          appConfig.Logging.DefaultLevel,
          appConfig.Logging.MicrosoftOverride));

      builder.Services.AddServiceDiscovery();

      OpenTelemetryConfiguration.AddTo(
        builder,
        appConfig.OpenTelemetry,
        [Telemetry.ScenariosRootName, Telemetry.ScenarioActivityName, Telemetry.ScenarioStepActivityName]); //bug needed?
      HealthChecksConfiguration.AddTo(builder);


      //bug remove these registrations
      builder.Services.AddSingleton(appConfig);
      builder.Services.AddSingleton<ServiceLogicRoot>(ctx => new ServiceLogicRoot(
        appConfig,
        ctx.GetRequiredService<IServiceDiscoveryHttpMessageHandlerFactory>(),
        ctx.GetRequiredService<ILoggerFactory>()));
      builder.Services.AddSingleton<TrafficGeneratorService>(ctx =>
        ctx.GetRequiredService<ServiceLogicRoot>().TrafficGeneratorService);

      builder.Services.AddRazorComponents()
        .AddInteractiveServerComponents();

      var app = builder.Build();

      if (!app.Environment.IsDevelopment())
      {
        app.UseExceptionHandler("/Error", createScopeForErrors: true);
        app.UseHsts();
      }

      OpenTelemetryConfiguration.Map(app);
      HealthChecksConfiguration.Map(app);

      app.UseAntiforgery();
      app.MapStaticAssets();
      app.MapRazorComponents<App>()
        .AddInteractiveServerRenderMode();

      app.Run();
    }
    catch (Exception ex) when (ex is not HostAbortedException)
    {
      Log.Fatal(ex, "Application terminated unexpectedly");
      throw;
    }
    finally
    {
      Log.CloseAndFlush();
    }

// bug
  }
}