using System.Collections.Concurrent;
using System.Diagnostics;

namespace DemoUiServiceSpecification.Automation;

public sealed class TestActivityListener : IDisposable
{
  private readonly ActivityListener _listener;

  private ConcurrentBag<Activity> Activities { get; } = [];

  public TestActivityListener()
  {
    _listener = new ActivityListener
    {
      ShouldListenTo = _ => true,
      Sample = (ref _) =>
        ActivitySamplingResult.AllDataAndRecorded,
      ActivityStarted = activity => Activities.Add(activity),

      ActivityStopped = _ => { }
    };

    ActivitySource.AddActivityListener(_listener);
  }

  public void Dispose()
  {
    _listener.Dispose();
  }
}