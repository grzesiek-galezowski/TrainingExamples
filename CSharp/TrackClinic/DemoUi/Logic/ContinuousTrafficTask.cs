using DemoUi.Dto;

namespace DemoUi.Logic;

internal interface IContinuousTrafficTask
{
  bool IsRunning { get; }
  Task StartAsync(int concurrency, int delayBetweenScenariosMs, IStateChangeObserver stateChangeObserver);
  Task StopAsync(IStateChangeObserver stateChangeObserver);
  Task FinishAsync();
  int GetActiveConcurrency();
}

internal sealed class ContinuousTrafficTask : IContinuousTrafficTask
{
  private readonly TrackClinicApiClient _client;
  private readonly ILogger<TrafficGeneratorService> _logger;
  private readonly Action<ScenarioExecutionResultDto> _recordScenarioResult;
  private CancellationTokenSource? _cancellationTokenSource;
  private int _activeConcurrency; //bug take it from constructor
  private Task? _task;

  public ContinuousTrafficTask(
    TrackClinicApiClient client,
    ILogger<TrafficGeneratorService> logger, //bug
    Action<ScenarioExecutionResultDto> recordScenarioResult) //bug
  {
    _client = client;
    _logger = logger;
    _recordScenarioResult = recordScenarioResult;
  }

  public bool IsRunning => _cancellationTokenSource != null && !_cancellationTokenSource.IsCancellationRequested;

  public async Task StartAsync(int concurrency, int delayBetweenScenariosMs, IStateChangeObserver stateChangeObserver)
  {
    if (IsRunning)
    {
      return;
    }

    _cancellationTokenSource = new CancellationTokenSource();
    _activeConcurrency = concurrency;
    var token = _cancellationTokenSource.Token;

    _task = Task.Run(
      async () =>
      {
        var workers = new List<Task>();
        for (var workerId = 0; workerId < concurrency; workerId++)
        {
          workers.Add(CreateWorker(workerId, delayBetweenScenariosMs, token));
        }

        try
        {
          await Task.WhenAll(workers);
        }
        catch (OperationCanceledException)
        {
        }
        finally
        {
          _activeConcurrency = 0;
          await stateChangeObserver.Refresh();
        }
      },
      token);

    await stateChangeObserver.Refresh();
  }

  public async Task StopAsync(IStateChangeObserver stateChangeObserver)
  {
    await (_cancellationTokenSource?.CancelAsync() ?? Task.CompletedTask);
    _activeConcurrency = 0;
    await stateChangeObserver.Refresh();
  }

  public async Task FinishAsync()
  {
    await (_cancellationTokenSource?.CancelAsync() ?? Task.CompletedTask);
    await (_task ?? Task.CompletedTask);
    _cancellationTokenSource?.Dispose();
  }

  public int GetActiveConcurrency()
  {
    return _activeConcurrency;
  }

  private Task CreateWorker(int workerId, int delayBetweenScenariosMs, CancellationToken token)
  {
    return Task.Run(
      async () =>
      {
        while (!token.IsCancellationRequested)
        {
          try
          {
            var preset = SeededScenarioPreset.CreateRandomInstanceWithConsistentId();
            var result = await _client.ExecuteScenarioAsync(
              preset.Artist,
              preset.Song1,
              preset.Song2,
              token);

            _recordScenarioResult(result);

            if (delayBetweenScenariosMs > 0)
            {
              await Task.Delay(delayBetweenScenariosMs, token);
            }
          }
          catch (OperationCanceledException)
          {
            break;
          }
          catch (Exception ex)
          {
            _logger.LogError(ex, "Continuous worker {WorkerId} encountered an error", workerId);
            try
            {
              await Task.Delay(500, token);
            }
            catch
            {
              break;
            }
          }
        }
      },
      token);
  }

}
