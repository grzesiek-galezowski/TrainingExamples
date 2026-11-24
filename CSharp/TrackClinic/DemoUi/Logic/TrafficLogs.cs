using System.Collections.Concurrent;

namespace DemoUi.Logic;

public class TrafficLogs //bug
{
  private readonly ConcurrentQueue<TrafficLogItem> _logs = new();
  private const int MaxLogCount = 150;
  
  public IReadOnlyList<TrafficLogItem> GetLogItems()
  {
    return [.. _logs];
  }

  public void Reset()
  {
    while (_logs.TryDequeue(out _)) { }
  }

  public void Add(TrafficLogItem log)
  {
    _logs.Enqueue(log);
    TrimLogCountTo(MaxLogCount);
  }

  private void TrimLogCountTo(int maxLogCount)
  {
    while (_logs.Count > maxLogCount && _logs.TryDequeue(out _)) { }
  }
}