namespace DemoUi.Logic;

public class TrafficStats
{
  public long TotalScenarios { get; set; }
  public long SuccessfulScenarios { get; set; }
  public long FailedScenarios { get; set; }
  public long TotalRequests { get; set; }
  public long SuccessfulRequests { get; set; }
  public long FailedRequests { get; set; }
  public double CurrentRps { get; set; }
  public double AverageLatencyMs { get; set; }
  public bool IsContinuousRunning { get; set; }
  public int ActiveConcurrency { get; set; }
}