using DemoUi;
using DemoUi.Adapters.Configuration;
using DemoUi.Adapters.Keycloak;
using DemoUi.Components;
using DemoUi.Logic;
using Flurl.Http;
using Microsoft.Extensions.ServiceDiscovery.Http;
using Serilog;
using ServiceBuildingBlocks;

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

  builder.Services.AddSingleton<IFlurlClient>(p => CreateFlurlClientWithServiceDiscovery(
    p,
    appConfig.TrackClinic.BaseUrl,
    TimeSpan.FromSeconds(appConfig.TrackClinic.TimeoutSeconds)));
  builder.Services.AddTransient<TrackClinicApiClient>();
  builder.Services.AddTransient<KeycloakTokenClient>();
  builder.Services.AddSingleton<TrafficGeneratorService>();

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
static IFlurlClient CreateFlurlClientWithServiceDiscovery(IServiceProvider serviceProvider, Uri baseUrl, TimeSpan timeout)
{
  var serviceDiscoveryHandlerFactory = serviceProvider.GetRequiredService<IServiceDiscoveryHttpMessageHandlerFactory>();
  var baseUrlString = baseUrl.ToString();
  var baseUri = new Uri(baseUrlString.EndsWith('/') ? baseUrlString : baseUrlString + "/");
  var httpClient = new HttpClient(serviceDiscoveryHandlerFactory.CreateHandler(new HttpClientHandler()))
  {
    BaseAddress = baseUri,
    Timeout = timeout
  };
  return new FlurlClient(httpClient);
}
