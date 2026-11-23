using System.Collections.Concurrent;
using System.Diagnostics;
using DemoUi.Dto;

namespace DemoUi.Logic;

public class TrafficGeneratorService : IAsyncDisposable
{
  private readonly TrackClinicApiClient _client;
  private readonly ILogger<TrafficGeneratorService> _logger;
  private readonly ConcurrentQueue<TrafficLogItem> _logs = new();
  private const int MaxLogCount = 150;
  private readonly ScenarioStats _scenarioStats = new();

  // RPS calculation
  private readonly ConcurrentQueue<long> _requestTimestamps = new();
  private readonly IContinuousTrafficTask _continuousTask;
  private readonly ITrafficStatsTask _statsTask;
  private IStateChangeObserver _stateChangeObserver = new NullStateChangeObserver();

  public TrafficGeneratorService(TrackClinicApiClient client, ILogger<TrafficGeneratorService> logger)
  {
    _client = client;
    _logger = logger;
    _continuousTask = new ContinuousTrafficTask(
      _client,
      _logger,
      RecordScenarioResult);
    _statsTask = new TrafficStatsTask(
      _logger,
      () => _stateChangeObserver,
      CleanOldTimestamps);
  }

  public IReadOnlyList<TrafficLogItem> RecentLogs => [.. _logs];

  public TrafficStats GetStats()
  {
    CleanOldTimestamps();
    var (totalReq, totalLat) = _scenarioStats.ReadRequestsAndLatency();
    var avgLatency = totalReq > 0 ? (double)totalLat / totalReq : 0.0;

    return new TrafficStats
    {
      TotalScenarios = _scenarioStats.ReadTotalScenarios(),
      SuccessfulScenarios = _scenarioStats.ReadSuccessfulScenarios(),
      FailedScenarios = _scenarioStats.ReadFailedScenarios(),
      TotalRequests = totalReq,
      SuccessfulRequests = _scenarioStats.ReadSuccessfulRequests(),
      FailedRequests = _scenarioStats.ReadFailedRequests(),
      CurrentRps = _requestTimestamps.Count,
      AverageLatencyMs = Math.Round(avgLatency, 1),
      IsContinuousRunning = _continuousTask.IsRunning,
      ActiveConcurrency = _continuousTask.GetActiveConcurrency()
    };
  }

  public async Task<ScenarioExecutionResultDto> RunSingleScenario(SeededScenarioPreset seededScenarioPreset, CancellationToken cancellationToken)
  {
    var result = await _client.ExecuteScenarioAsync(
      seededScenarioPreset.Artist,
      seededScenarioPreset.Song1,
      seededScenarioPreset.Song2,
      cancellationToken);
    RecordScenarioResult(result);
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
          RecordScenarioResult(result);

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
    _scenarioStats.Reset();

    while (_requestTimestamps.TryDequeue(out _)) { }
    while (_logs.TryDequeue(out _)) { }

    _stateChangeObserver.Refresh();
  }

  private void RecordScenarioResult(ScenarioExecutionResultDto result)
  {
    _scenarioStats.RecordScenario(result.Success);

    var now = Stopwatch.GetTimestamp();

    foreach (var step in result.Steps)
    {
      _scenarioStats.RecordRequest(step.IsSuccess, step.ElapsedMilliseconds);
      _requestTimestamps.Enqueue(now);

      AddLog(new TrafficLogItem(
        Timestamp: step.Timestamp,
        ScenarioId: result.ScenarioId,
        HttpMethod: step.HttpMethod,
        Endpoint: step.Url,
        StatusCode: step.StatusCode,
        ElapsedMilliseconds: step.ElapsedMilliseconds,
        Success: step.IsSuccess,
        Details: step.IsSuccess ? step.ActionName : $"{step.ActionName}: {step.ErrorMessage}"
      ));
    }
  }

  private void AddLog(TrafficLogItem log)
  {
    _logs.Enqueue(log);
    while (_logs.Count > MaxLogCount && _logs.TryDequeue(out _)) { } //bug what is this?
  }

  private void CleanOldTimestamps()
  {
    var cutoff = Stopwatch.GetTimestamp() - Stopwatch.Frequency; // 1 second ago
    while (_requestTimestamps.TryPeek(out var timestamp) && timestamp < cutoff)
    {
      _requestTimestamps.TryDequeue(out _);
    }
  }

  public async ValueTask DisposeAsync()
  {
    await FinishStatsTask();
    await FinishContinuousTask();
  }

  private async ValueTask FinishContinuousTask()
  {
    await _continuousTask.FinishAsync();
  }

  private async ValueTask FinishStatsTask()
  {
    await _statsTask.FinishAsync();
  }

  public void RegisterObserver(IStateChangeObserver stateChangeObserver)
  {
    _stateChangeObserver = stateChangeObserver;
  }
}
