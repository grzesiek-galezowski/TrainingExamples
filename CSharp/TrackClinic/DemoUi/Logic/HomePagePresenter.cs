using System.Globalization;
using DemoUi.Dto;

namespace DemoUi.Logic;

public class HomePagePresenter(TrafficGeneratorService trafficService) : IDisposable
{
  private readonly CancellationTokenSource _cts = new();

  public int Concurrency { get; set; } = 5;
  public int DelayMs { get; set; } = 100;
  public int BatchCount { get; set; } = 25;

  public bool IsSingleRunning { get; private set; }
  public bool IsBatchRunning { get; private set; }
  public ScenarioExecutionResultDto? LastSingleResult { get; private set; }
  public SeededScenarioPresetDto Preset { get; private set; } = new("Pink Floyd", "Comfortably Numb", "Echoes");

  public TrafficStats Stats => trafficService.GetStats();
  public IReadOnlyList<TrafficLogItem> RecentLogs => trafficService.RecentLogs;

  public void Initialize(IStateChangeObserver stateChangeObserver)
  {
    trafficService.RegisterObserver(stateChangeObserver);
    PickRandomPreset();
  }

  public void PickRandomPreset()
  {
    Preset = new SeededScenarioPresetDto(SeededScenarioPreset.CreateRandomInstanceWithConsistentId());
  }

  public async Task RunSingle()
  {
    if (IsSingleRunning) return;
    IsSingleRunning = true;
    try
    {
      LastSingleResult = await trafficService.RunSingleScenario(Preset.ToSeededScenarioPreset(), _cts.Token);
    }
    catch (Exception ex)
    {
      LastSingleResult = ScenarioExecutionResultDto.Error(Preset.ToSeededScenarioPreset(), ex);
    }
    finally
    {
      IsSingleRunning = false;
    }
  }

  public async Task RunBatch()
  {
    if (IsBatchRunning) return;
    IsBatchRunning = true;
    try
    {
      await trafficService.RunBatch(BatchCount, Concurrency, DelayMs, _cts.Token);
    }
    finally
    {
      IsBatchRunning = false;
    }
  }

  public async Task StartContinuous()
  {
    await trafficService.StartContinuous(Concurrency, DelayMs);
  }

  public async Task StopContinuous()
  {
    await trafficService.StopContinuous();
  }

  public void ResetStats()
  {
    LastSingleResult = null;
    trafficService.ResetStats();
  }

  public string GetSuccessRate()
  {
    if (Stats.TotalRequests == 0) return "100";
    var rate = (double)Stats.SuccessfulRequests / Stats.TotalRequests * 100.0;
    return rate.ToString("F1", CultureInfo.InvariantCulture);
  }

  public void Dispose()
  {
    _cts.Cancel();
    _cts.Dispose();
  }
}