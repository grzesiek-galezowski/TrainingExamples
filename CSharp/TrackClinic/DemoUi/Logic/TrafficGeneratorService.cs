using System.Collections.Concurrent;
using System.Diagnostics;
using DemoUi.Dto;

namespace DemoUi.Logic;

//bug refactor this
public class TrafficGeneratorService : IAsyncDisposable
{
  private readonly TrackClinicApiClient _client;
  private readonly ILogger<TrafficGeneratorService> _logger;
  private readonly ConcurrentQueue<TrafficLogItem> _logs = new();
  private const int MaxLogCount = 150;
  private readonly ScenarioStats _scenarioStats = new();

  private CancellationTokenSource? _continuousCts;
  private Task? _continuousTask; //bug unused
  private int _activeConcurrency;

  // RPS calculation
  private readonly ConcurrentQueue<long> _requestTimestamps = new();
  private readonly CancellationTokenSource _statsCts = new();
  private readonly Task? _statsTask;
  private IStateChangeObserver _stateChangeObserver = new NullStateChangeObserver();

  public TrafficGeneratorService(TrackClinicApiClient client, ILogger<TrafficGeneratorService> logger)
  {
    _client = client;
    _logger = logger;

    // Periodic timer to clean old timestamps and refresh UI
    _statsTask = Task.Run(async () =>
    {
      using var periodicTimer = new PeriodicTimer(TimeSpan.FromSeconds(1));
      try
      {
        while (await periodicTimer.WaitForNextTickAsync(_statsCts.Token))
        {
          try
          {
            CleanOldTimestamps();
            await _stateChangeObserver.Refresh();
          }
          catch (OperationCanceledException)
          {
            // Normal shutdown
          }
          catch (Exception ex)
          {
            _logger.LogError(ex, "Error in stats refresh timer");
          }
        }
      }
      catch(OperationCanceledException)
      {
        // Normal shutdown
      }
    });
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
      IsContinuousRunning = IsContinuousRunInProgress(),
      ActiveConcurrency = _activeConcurrency
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
    if (!IsContinuousRunInProgress())
    {
      _continuousCts = new CancellationTokenSource();
      _activeConcurrency = concurrency;
      var token = _continuousCts.Token;

      _continuousTask = Task.Run(
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
            await _stateChangeObserver.Refresh();
          }
        },
        token);

      await _stateChangeObserver.Refresh();
    }
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
            RecordScenarioResult(result);

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

  public async Task StopContinuous()
  {
    await (_continuousCts?.CancelAsync() ?? Task.CompletedTask);
    _activeConcurrency = 0;
    await _stateChangeObserver.Refresh();
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

  private bool IsContinuousRunInProgress()
  {
    return _continuousCts != null && !_continuousCts.IsCancellationRequested;
  }

  public async ValueTask DisposeAsync()
  {
    await _statsCts.CancelAsync();
    await (_statsTask ?? Task.CompletedTask);
    _statsCts.Dispose();
    await (_continuousCts?.CancelAsync() ?? Task.CompletedTask);
    await (_continuousTask ?? Task.CompletedTask);
    _continuousCts?.Dispose();
  }

  public void RegisterObserver(IStateChangeObserver stateChangeObserver)
  {
    _stateChangeObserver = stateChangeObserver;
  }
}
