using DemoUi;
using DemoUi.Adapters.Configuration;
using DemoUi.Components;
using DemoUi.Logic;
using Microsoft.Extensions.ServiceDiscovery.Http;
using Serilog;
using ServiceBuildingBlocks;

public class Program
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
        [DemoUiTelemetry.ScenariosRootName, DemoUiTelemetry.ScenarioActivityName, DemoUiTelemetry.ScenarioStepActivityName]); //bug needed?
      HealthChecksConfiguration.AddTo(builder);

      builder.Services.AddSingleton(appConfig);
      builder.Services.AddSingleton<ServiceLogicRoot>(ctx => new ServiceLogicRoot(
        appConfig,
        ctx.GetRequiredService<IServiceDiscoveryHttpMessageHandlerFactory>(),
        ctx.GetRequiredService<ILoggerFactory>()));
      builder.Services.AddTransient<HomePagePresenter>(ctx =>
        ctx.GetRequiredService<ServiceLogicRoot>().CreateHomePagePresenter());

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