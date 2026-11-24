using System.Collections.Concurrent;
using System.Diagnostics;
using DemoUi.Dto;

namespace DemoUi.Logic;

public class TrafficMetrics //bug
{
  private readonly AggregatedScenarioCounters _aggregatedScenarioCounters = new();
  private readonly ConcurrentQueue<long> _requestTimestamps = new();

  public void CleanOldTimestamps()
  {
    var cutoff = Stopwatch.GetTimestamp() - Stopwatch.Frequency; // 1 second ago
    while (_requestTimestamps.TryPeek(out var timestamp) && timestamp < cutoff)
    {
      _requestTimestamps.TryDequeue(out _);
    }
  }

  public void Reset()
  {
    _aggregatedScenarioCounters.Reset();

    while (_requestTimestamps.TryDequeue(out _)) { }
  }

  public void RecordScenarioResult(TrafficLogs trafficLogs, ScenarioExecutionResultDto result)
  {
    _aggregatedScenarioCounters.RecordScenario(result.Success);

    var now = Stopwatch.GetTimestamp();

    foreach (var step in result.Steps)
    {
      _aggregatedScenarioCounters.RecordRequest(step.IsSuccess, step.ElapsedMilliseconds);
      _requestTimestamps.Enqueue(now);

      trafficLogs.Add(TrafficLogItem.From(result, step));
    }
  }

  public TrafficStats CreateTrafficStats(bool continuousTaskIsRunning, int activeConcurrency)
  {
    var (totalReq, totalLat) = _aggregatedScenarioCounters.ReadRequestsAndLatency();
    var avgLatency = totalReq > 0 ? (double)totalLat / totalReq : 0.0;

    return new TrafficStats
    {
      TotalScenarios = _aggregatedScenarioCounters.ReadTotalScenarios(),
      SuccessfulScenarios = _aggregatedScenarioCounters.ReadSuccessfulScenarios(),
      FailedScenarios = _aggregatedScenarioCounters.ReadFailedScenarios(),
      TotalRequests = totalReq,
      SuccessfulRequests = _aggregatedScenarioCounters.ReadSuccessfulRequests(),
      FailedRequests = _aggregatedScenarioCounters.ReadFailedRequests(),
      CurrentRps = _requestTimestamps.Count,
      AverageLatencyMs = Math.Round(avgLatency, 1),
      IsContinuousRunning = continuousTaskIsRunning,
      ActiveConcurrency = activeConcurrency
    };
  }
}