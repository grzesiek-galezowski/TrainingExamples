namespace DemoUi.Logic;

internal interface ITrafficStatsTask
{
  Task FinishAsync();
}

internal sealed class TrafficStatsTask : ITrafficStatsTask
{
  private readonly ILogger<TrafficGeneratorService> _logger;
  private readonly Func<IStateChangeObserver> _getStateChangeObserver;
  private readonly Action _cleanOldTimestamps;
  private readonly CancellationTokenSource _cancellationTokenSource = new();
  private readonly Task _task;

  public TrafficStatsTask(
    ILogger<TrafficGeneratorService> logger,
    Func<IStateChangeObserver> getStateChangeObserver,
    Action cleanOldTimestamps)
  {
    _logger = logger;
    _getStateChangeObserver = getStateChangeObserver;
    _cleanOldTimestamps = cleanOldTimestamps;

    _task = Task.Run(async () =>
    {
      using var periodicTimer = new PeriodicTimer(TimeSpan.FromSeconds(1));
      try
      {
        while (await periodicTimer.WaitForNextTickAsync(_cancellationTokenSource.Token))
        {
          try
          {
            _cleanOldTimestamps();
            await _getStateChangeObserver().Refresh();
          }
          catch (OperationCanceledException)
          {
          }
          catch (Exception ex)
          {
            _logger.LogError(ex, "Error in stats refresh timer");
          }
        }
      }
      catch (OperationCanceledException)
      {
      }
    });
  }

  public async Task FinishAsync()
  {
    await _cancellationTokenSource.CancelAsync();
    await _task;
    _cancellationTokenSource.Dispose();
  }
}
