namespace DemoUi.Logic;

public class ScenarioStats
{
  private long _totalScenarios;
  private long _successfulScenarios;
  private long _failedScenarios;
  private long _totalRequests;
  private long _successfulRequests;
  private long _failedRequests;
  private long _totalLatencyMs;

  public void RecordScenario(bool success)
  {
    Interlocked.Increment(ref _totalScenarios);
    if (success)
    {
      Interlocked.Increment(ref _successfulScenarios);
    }
    else
    {
      Interlocked.Increment(ref _failedScenarios);
    }
  }

  public void RecordRequest(bool success, long elapsedMilliseconds)
  {
    Interlocked.Increment(ref _totalRequests);
    if (success)
    {
      Interlocked.Increment(ref _successfulRequests);
    }
    else
    {
      Interlocked.Increment(ref _failedRequests);
    }

    Interlocked.Add(ref _totalLatencyMs, elapsedMilliseconds);
  }

  public void Reset()
  {
    Interlocked.Exchange(ref _totalScenarios, 0);
    Interlocked.Exchange(ref _successfulScenarios, 0);
    Interlocked.Exchange(ref _failedScenarios, 0);
    Interlocked.Exchange(ref _totalRequests, 0);
    Interlocked.Exchange(ref _successfulRequests, 0);
    Interlocked.Exchange(ref _failedRequests, 0);
    Interlocked.Exchange(ref _totalLatencyMs, 0);
  }

  public (long TotalRequests, long TotalLatencyMs) ReadRequestsAndLatency()
  {
    var totalReq = Interlocked.Read(ref _totalRequests);
    var totalLat = Interlocked.Read(ref _totalLatencyMs);
    return (totalReq, totalLat);
  }

  public long ReadTotalScenarios() => Interlocked.Read(ref _totalScenarios);
  public long ReadSuccessfulScenarios() => Interlocked.Read(ref _successfulScenarios);
  public long ReadFailedScenarios() => Interlocked.Read(ref _failedScenarios);
  public long ReadSuccessfulRequests() => Interlocked.Read(ref _successfulRequests);
  public long ReadFailedRequests() => Interlocked.Read(ref _failedRequests);
}
