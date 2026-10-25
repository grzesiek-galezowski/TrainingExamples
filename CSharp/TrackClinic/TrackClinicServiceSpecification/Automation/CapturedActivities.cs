using System.Collections.Concurrent;
using System.Diagnostics;
using AwesomeAssertions;
using Core.Maybe;

namespace TrackClinicServiceSpecification.Automation;

public class CapturedActivities : IDisposable
{
  private readonly ActivityListener _listener;
  private readonly ConcurrentBag<Activity> _activities = [];

  public CapturedActivities()
  {
    _listener = new ActivityListener
    {
      ShouldListenTo = source => source.Name == "Microsoft.AspNetCore",
      Sample = (ref _) => ActivitySamplingResult.AllData,
      ActivityStopped = activity => _activities.Add(activity)
    };
    ActivitySource.AddActivityListener(_listener);
  }

  public async Task ShouldHaveCapturedTrace(string traceId, string expectedMethod, int expectedStatusCode)
  {
    var timeout = TimeSpan.FromSeconds(5);
    var activity = await PollForTrace(traceId, timeout); //bug use fluent assertions for that

    activity.IsSomething().Should().BeTrue(
      $"Activity with Trace ID {traceId} should have been captured within {timeout}.\nCaptured activities:\n{CapturedActivitiesString()}");

    activity.Value().TagObjects.Should().Contain(tag => HttpRequestMethod(expectedMethod, tag));
    activity.Value().TagObjects.Should().Contain(tag => ResponseStatusCode(expectedStatusCode, tag));
  }

  private static bool ResponseStatusCode(int expectedStatusCode, KeyValuePair<string, object?> tag)
  {
    return tag.Key == "http.response.status_code" && Equals(tag.Value, expectedStatusCode);
  }

  private static bool HttpRequestMethod(string expectedMethod, KeyValuePair<string, object?> tag)
  {
    return tag.Key == "http.request.method" && Equals(tag.Value, expectedMethod);
  }

  private string CapturedActivitiesString()
  {
    return string.Join(
      "\n",
      _activities.ToList().Select(a =>
        $"{a.TraceId.ToHexString()} - {a.OperationName} - {a.TagObjects.FirstOrDefault(t => t.Key == "http.request.method").Value} {a.TagObjects.FirstOrDefault(t => t.Key == "url.path").Value}"));
  }

  private async Task<Maybe<Activity>> PollForTrace(string traceId, TimeSpan timeout)
  {
    using var cts = new CancellationTokenSource(timeout);
    using var timer = new PeriodicTimer(TimeSpan.FromMilliseconds(100));

    try
    {
      do
      {
        var activity = _activities.FirstMaybe(a => a.TraceId.ToHexString() == traceId);
        if (activity.HasValue)
        {
          return activity;
        }
      } while (await timer.WaitForNextTickAsync(cts.Token));
    }
    catch (OperationCanceledException)
    {
    }

    return Maybe<Activity>.Nothing;
  }

  public void Dispose()
  {
    _listener.Dispose();
  }
}