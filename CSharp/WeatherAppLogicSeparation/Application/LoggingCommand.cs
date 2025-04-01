using System.Collections.Immutable;
using Serilog.Context;
using Serilog.Core;
using ILogger = Serilog.ILogger;

namespace Application;

public class LoggingCommand(ILogger logger, IWeatherAppCommand next, ImmutableList<ILogEventEnricher> propertyEnrichers)
  : IWeatherAppCommand
{
  public async Task Execute(CancellationToken token)
  {
    using (LogContext.Push(propertyEnrichers.ToArray()))
    {
      try
      {
        logger.Information("Executing command");
        await next.Execute(token);
      }
      finally
      {
        logger.Information("Finished executing command");
      }
    }
  }
}