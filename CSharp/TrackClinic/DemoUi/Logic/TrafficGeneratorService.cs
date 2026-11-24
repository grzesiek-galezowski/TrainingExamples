using DemoUi.Dto;

namespace DemoUi.Logic;

public class TrafficGeneratorService : IAsyncDisposable
{
  private readonly TrackClinicApiClient _client;
  private readonly ILogger<TrafficGeneratorService> _logger;

  // RPS calculation
  private readonly IContinuousTrafficTask _continuousTask;
  private readonly ITrafficStatsTask _statsTask;
  private IStateChangeObserver _stateChangeObserver = new NullStateChangeObserver();
  private readonly TrafficMetrics _trafficMetrics;
  private readonly TrafficLogs _trafficLogs;

  public TrafficGeneratorService(TrackClinicApiClient client, ILogger<TrafficGeneratorService> logger)
  {
    _client = client;
    _logger = logger;
    _trafficMetrics = new TrafficMetrics();
    _trafficLogs = new TrafficLogs();
    _continuousTask = new ContinuousTrafficTask(
      _client,
      _trafficMetrics,
      _trafficLogs,
      _logger);
    _statsTask = new TrafficStatsTask(
      _logger,
      () => _stateChangeObserver,
      () => _trafficMetrics.CleanOldTimestamps());
  }

  public IReadOnlyList<TrafficLogItem> RecentLogs => _trafficLogs.GetLogItems();

  public TrafficStats GetStats()
  {
    _trafficMetrics.CleanOldTimestamps();
    return _trafficMetrics.CreateTrafficStats(
      _continuousTask.IsRunning,
      _continuousTask.GetActiveConcurrency());
  }

  public async Task<ScenarioExecutionResultDto> RunSingleScenario(SeededScenarioPreset seededScenarioPreset, CancellationToken cancellationToken)
  {
    var result = await _client.ExecuteScenarioAsync(
      seededScenarioPreset.Artist,
      seededScenarioPreset.Song1,
      seededScenarioPreset.Song2,
      cancellationToken);
    _trafficMetrics.RecordScenarioResult(_trafficLogs, result);
    await _stateChangeObserver.Refresh();
    return result;
  }
  
  public async Task RunBatch(int scenarioCount, int concurrency, int delayMs, CancellationToken cancellationToken)
  {
    var semaphore = new SemaphoreSlim(concurrency, concurrency);
    var tasks = new List<Task>();

    for (var i = 0; i < scenarioCount; i++)
    {
      await semaphore.WaitAsync(cancellationToken);
      tasks.Add(Task.Run(async () =>
      {
        try
        {
          var preset = SeededScenarioPreset.CreateRandomInstanceWithConsistentId();
          var result = await _client.ExecuteScenarioAsync(preset.Artist, preset.Song1, preset.Song2, cancellationToken);
          _trafficMetrics.RecordScenarioResult(_trafficLogs, result);

          await Task.Delay(delayMs, cancellationToken);
        }
        catch (Exception ex)
        {
          _logger.LogError(ex, "Error in batch scenario worker");
        }
        finally
        {
          semaphore.Release();
        }
      },
      cancellationToken));
    }

    await Task.WhenAll(tasks);
    await _stateChangeObserver.Refresh();
  }

  public async Task StartContinuous(int concurrency, int delayBetweenScenariosMs)
  {
    await _continuousTask.StartAsync(concurrency, delayBetweenScenariosMs, _stateChangeObserver);
  }

  public async Task StopContinuous()
  {
    await _continuousTask.StopAsync(_stateChangeObserver);
  }

  public void ResetStats()
  {
    _trafficMetrics.Reset();
    _trafficLogs.Reset();
    _stateChangeObserver.Refresh();
  }

  public async ValueTask DisposeAsync()
  {
    await _statsTask.FinishAsync();
    await _continuousTask.FinishAsync();
  }

  public void RegisterObserver(IStateChangeObserver stateChangeObserver)
  {
    _stateChangeObserver = stateChangeObserver;
  }
}
