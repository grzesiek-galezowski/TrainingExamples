using Serilog;
using Serilog.Events;
using Serilog.Extensions.Hosting;
using Serilog.Formatting.Json;

namespace TrackClinic;

public static class LoggingConfigurations
{
  public static ReloadableLogger ConfigureBootstrapLogger(LoggerConfiguration loggerConfiguration)
  {
    return loggerConfiguration
      .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
      .Enrich.FromLogContext()
      .WriteTo.Console(new JsonFormatter())
      .CreateBootstrapLogger();
  }

  public static void ConfigureServiceLogging(LoggerConfiguration configuration, IServiceProvider services,
    LogEventLevel loggingDefaultLevel, LogEventLevel loggingMicrosoftOverride)
  {
    configuration
      .MinimumLevel.Is(loggingDefaultLevel)
      .MinimumLevel.Override("Microsoft", loggingMicrosoftOverride)
      .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
      .Enrich.FromLogContext()
      .ReadFrom.Services(services)
      .WriteTo.Console(new JsonFormatter());
  }
}