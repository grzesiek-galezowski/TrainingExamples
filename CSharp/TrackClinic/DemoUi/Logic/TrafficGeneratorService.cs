using System.Collections.Concurrent;
using System.Diagnostics;
using DemoUi.Dto;

namespace DemoUi.Logic;

//bug refactor this
public class TrafficGeneratorService : IDisposable
{
  private readonly IServiceProvider _serviceProvider;
  private readonly ILogger<TrafficGeneratorService> _logger;

  private readonly ConcurrentQueue<TrafficLogItem> _logs = new();
  private readonly int _maxLogCount = 150;

  private long _totalScenarios;
  private long _successfulScenarios;
  private long _failedScenarios;
  private long _totalRequests;
  private long _successfulRequests;
  private long _failedRequests;
  private long _totalLatencyMs;

  private CancellationTokenSource? _continuousCts;
  private Task? _continuousTask;
  private int _activeConcurrency;
  private bool _disposed;

  // RPS calculation
  private readonly ConcurrentQueue<long> _requestTimestamps = new();
  private readonly Timer _statsTimer;

  public event Action? OnStateChanged;

  public TrafficGeneratorService(IServiceProvider serviceProvider, ILogger<TrafficGeneratorService> logger)
  {
    _serviceProvider = serviceProvider;
    _logger = logger;

    // Periodic timer to clean old timestamps and refresh UI
    _statsTimer = new Timer(_ =>
    {
      CleanOldTimestamps();
      OnStateChanged?.Invoke();
    }, null, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));
  }

  public IReadOnlyList<TrafficLogItem> RecentLogs => [.. _logs];

  public TrafficStats GetStats()
  {
    CleanOldTimestamps();
    var totalReq = Interlocked.Read(ref _totalRequests);
    var totalLat = Interlocked.Read(ref _totalLatencyMs);
    var avgLatency = totalReq > 0 ? (double)totalLat / totalReq : 0.0;

    return new TrafficStats
    {
      TotalScenarios = Interlocked.Read(ref _totalScenarios),
      SuccessfulScenarios = Interlocked.Read(ref _successfulScenarios),
      FailedScenarios = Interlocked.Read(ref _failedScenarios),
      TotalRequests = totalReq,
      SuccessfulRequests = Interlocked.Read(ref _successfulRequests),
      FailedRequests = Interlocked.Read(ref _failedRequests),
      CurrentRps = _requestTimestamps.Count,
      AverageLatencyMs = Math.Round(avgLatency, 1),
      IsContinuousRunning = _continuousCts != null && !_continuousCts.IsCancellationRequested,
      ActiveConcurrency = _activeConcurrency
    };
  }

  public async Task<ScenarioExecutionResultDto> RunSingleScenarioAsync(string? artist = null, string? song1 = null, string? song2 = null)
  {
    if (string.IsNullOrWhiteSpace(artist) || string.IsNullOrWhiteSpace(song1) || string.IsNullOrWhiteSpace(song2))
    {
      var preset = SeededData.GetRandomPreset();
      var id = Random.Shared.Next(100, 999);
      artist = $"{preset.Artist} ({id})";
      song1 = $"{preset.Song1} #{id}";
      song2 = $"{preset.Song2} #{id}";
    }

    using var scope = _serviceProvider.CreateScope();
    var client = scope.ServiceProvider.GetRequiredService<TrackClinicApiClient>();

    var result = await client.ExecuteScenarioAsync(artist, song1, song2);
    RecordScenarioResult(result);
    OnStateChanged?.Invoke();
    return result;
  }

  public async Task RunBatchAsync(int scenarioCount, int concurrency, int delayMs = 50)
  {
    var semaphore = new SemaphoreSlim(concurrency, concurrency);
    var tasks = new List<Task>();

    for (var i = 0; i < scenarioCount; i++)
    {
      await semaphore.WaitAsync();
      tasks.Add(Task.Run(async () =>
      {
        try
        {
          var preset = SeededData.GetRandomPreset();
          var id = Random.Shared.Next(1000, 9999);
          var artist = $"{preset.Artist}_{id}";
          var song1 = $"{preset.Song1}_{id}";
          var song2 = $"{preset.Song2}_{id}";

          using var scope = _serviceProvider.CreateScope();
          var client = scope.ServiceProvider.GetRequiredService<TrackClinicApiClient>();
          var result = await client.ExecuteScenarioAsync(artist, song1, song2);
          RecordScenarioResult(result);

          if (delayMs > 0)
          {
            await Task.Delay(delayMs);
          }
        }
        catch (Exception ex)
        {
          _logger.LogError(ex, "Error in batch scenario worker");
        }
        finally
        {
          semaphore.Release();
        }
      }));
    }

    await Task.WhenAll(tasks);
    OnStateChanged?.Invoke();
  }

  public void StartContinuous(int concurrency, int delayBetweenScenariosMs = 100)
  {
    if (_continuousCts != null && !_continuousCts.IsCancellationRequested)
    {
      return;
    }

    _continuousCts = new CancellationTokenSource();
    _activeConcurrency = concurrency;
    var token = _continuousCts.Token;

    _continuousTask = Task.Run(async () =>
    {
      var workers = new List<Task>();
      for (var workerId = 0; workerId < concurrency; workerId++)
      {
        var wId = workerId;
        workers.Add(Task.Run(async () =>
        {
          while (!token.IsCancellationRequested)
          {
            try
            {
              var preset = SeededData.GetRandomPreset();
              var runId = Random.Shared.Next(1000, 99999);
              var artist = $"{preset.Artist}_{runId}";
              var song1 = $"{preset.Song1}_{runId}";
              var song2 = $"{preset.Song2}_{runId}";

              using var scope = _serviceProvider.CreateScope();
              var client = scope.ServiceProvider.GetRequiredService<TrackClinicApiClient>();
              var result = await client.ExecuteScenarioAsync(artist, song1, song2, token);
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
              _logger.LogError(ex, "Continuous worker {WorkerId} encountered an error", wId);
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
        }, token));
      }

      try
      {
        await Task.WhenAll(workers);
      }
      catch (OperationCanceledException) { }
      finally
      {
        _activeConcurrency = 0;
        OnStateChanged?.Invoke();
      }
    }, token);

    OnStateChanged?.Invoke();
  }

  public void StopContinuous()
  {
    _continuousCts?.Cancel();
    _activeConcurrency = 0;
    OnStateChanged?.Invoke();
  }

  public void ResetStats()
  {
    Interlocked.Exchange(ref _totalScenarios, 0);
    Interlocked.Exchange(ref _successfulScenarios, 0);
    Interlocked.Exchange(ref _failedScenarios, 0);
    Interlocked.Exchange(ref _totalRequests, 0);
    Interlocked.Exchange(ref _successfulRequests, 0);
    Interlocked.Exchange(ref _failedRequests, 0);
    Interlocked.Exchange(ref _totalLatencyMs, 0);

    while (_requestTimestamps.TryDequeue(out _)) { }
    while (_logs.TryDequeue(out _)) { }

    OnStateChanged?.Invoke();
  }

  private void RecordScenarioResult(ScenarioExecutionResultDto result)
  {
    Interlocked.Increment(ref _totalScenarios);
    if (result.Success)
    {
      Interlocked.Increment(ref _successfulScenarios);
    }
    else
    {
      Interlocked.Increment(ref _failedScenarios);
    }

    var now = Stopwatch.GetTimestamp();

    foreach (var step in result.Steps)
    {
      Interlocked.Increment(ref _totalRequests);
      if (step.IsSuccess)
      {
        Interlocked.Increment(ref _successfulRequests);
      }
      else
      {
        Interlocked.Increment(ref _failedRequests);
      }

      Interlocked.Add(ref _totalLatencyMs, step.ElapsedMilliseconds);
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
    while (_logs.Count > _maxLogCount && _logs.TryDequeue(out _)) { }
  }

  private void CleanOldTimestamps()
  {
    var cutoff = Stopwatch.GetTimestamp() - Stopwatch.Frequency; // 1 second ago
    while (_requestTimestamps.TryPeek(out var timestamp) && timestamp < cutoff)
    {
      _requestTimestamps.TryDequeue(out _);
    }
  }

  public void Dispose()
  {
    if (_disposed) return;
    _disposed = true;
    _statsTimer.Dispose();
    _continuousCts?.Cancel();
    _continuousCts?.Dispose();
  }
}
