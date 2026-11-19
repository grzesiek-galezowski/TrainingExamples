using System.Collections.Immutable;
using AwesomeAssertions;
using AwesomeAssertions.Extensions;
using Core.Maybe;
using Flurl.Http;
using ServiceSpecificationBuildingBlocks.Zipkin;

namespace TrackClinic.AppHostSpecification.Automation;

public class ZipkinDriverExtension(
  FlurlClient zipkinHttpClient,
  IZipkinClient zipkinClient,
  CancellationToken cancellationToken)
{
  public async Task ShouldHaveCapturedScenarioTrace(string artist, string song1, string song2)
  {
    var timeout = 20.Seconds();
    var traceSpans = await WaitForCompleteScenarioTrace(artist, song1, song2, timeout);

    traceSpans.Should().NotBeEmpty(
      $"Scenario execution for artist={artist}, song1={song1}, song2={song2} should have generated a trace in Zipkin within {timeout}");

    traceSpans.Should().Contain(
      s => s.Name == "scenario.execute" && HasScenarioTags(s, artist, song1, song2),
      $"A scenario.execute trace span should exist for artist={artist}, song1={song1}, song2={song2}");

    traceSpans.Where(s => s.Name == "step.execute")
      .Should().HaveCount(4, "All 4 scenario steps (create x2, get x2) should be captured in the trace");

    traceSpans.Where(s => IsFromService(s, "TrackClinic"))
      .Should().HaveCountGreaterThanOrEqualTo(4,
        "TrackClinic service spans for the 4 HTTP requests should be part of the distributed scenario trace");
  }

  private async Task<ImmutableList<ZipkinSpan>> WaitForCompleteScenarioTrace(
    string artist,
    string song1,
    string song2,
    TimeSpan timeout)
  {
    using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
    cts.CancelAfter(timeout);
    using var timer = new PeriodicTimer(250.Milliseconds());

    ImmutableList<ZipkinSpan> latestTraceSpans = [];

    try
    {
      do
      {
        try
        {
          var scenarioSpan = await ReadScenarioExecuteSpan(artist, song1, song2, cts);

          if (scenarioSpan.HasValue)
          {
            latestTraceSpans = await zipkinClient.GetTrace(scenarioSpan.Value().TraceId, cts.Token);
            if (HasCompleteScenarioSpans(latestTraceSpans))
            {
              return latestTraceSpans;
            }
          }
        }
        catch (OperationCanceledException) when (cts.IsCancellationRequested)
        {
          break;
        }
        catch
        {
          // Zipkin may still be receiving batches
        }
      } while (await timer.WaitForNextTickAsync(cts.Token));
    }
    catch (OperationCanceledException)
    {
    }

    return latestTraceSpans;
  }

  private async Task<Maybe<ZipkinSpan>> ReadScenarioExecuteSpan(string artist, string song1, string song2, CancellationTokenSource cts)
  {
    var demoUiSpans = await GetDemoUiSpans(cts.Token);
    var scenarioSpan = demoUiSpans.FirstOrDefault(s =>
      s.Name == "scenario.execute" && HasScenarioTags(s, artist, song1, song2)).ToMaybe();
    return scenarioSpan;
  }

  private async Task<ImmutableList<ZipkinSpan>> GetDemoUiSpans(CancellationToken ct)
  {
    var response = await zipkinHttpClient
      .Request("api", "v2", "traces")
      .SetQueryParams(new { serviceName = "DemoUi", lookback = "3600000", limit = 50 })
      .GetJsonAsync<List<List<ZipkinSpan>>>(cancellationToken: ct);

    if (response is null)
    {
      return [];
    }

    return [.. response.SelectMany(t => t)];
  }

  private static bool HasCompleteScenarioSpans(ImmutableList<ZipkinSpan> traceSpans) =>
    traceSpans.Count(s => s.Name == "step.execute") >= 4 &&
    traceSpans.Count(s => IsFromService(s, "TrackClinic")) >= 4;

  private static bool HasScenarioTags(ZipkinSpan span, string artist, string song1, string song2) =>
    span.Tags is not null &&
    span.Tags.TryGetValue("scenario.artist", out var artistTag) && artistTag == artist &&
    span.Tags.TryGetValue("scenario.song1", out var song1Tag) && song1Tag == song1 &&
    span.Tags.TryGetValue("scenario.song2", out var song2Tag) && song2Tag == song2;

  private static bool IsFromService(ZipkinSpan span, string expectedServiceName) =>
    span.LocalEndpoint?.ServiceName is { } serviceName &&
    string.Equals(serviceName, expectedServiceName, StringComparison.OrdinalIgnoreCase);
}
