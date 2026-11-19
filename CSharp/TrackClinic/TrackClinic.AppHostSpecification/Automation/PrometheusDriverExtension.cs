using System.Collections.Immutable;
using System.Text.Json;
using AwesomeAssertions;
using AwesomeAssertions.Extensions;
using Flurl.Http;

namespace TrackClinic.AppHostSpecification.Automation;

public class PrometheusDriverExtension(FlurlClient prometheusClient, CancellationToken cancellationToken)
{
  public async Task ShouldHaveScrapedMetricsFor(ImmutableList<string> expectedServiceNames)
  {
    var timeout = 30.Seconds();
    var scrapedServices = await WaitForScrapedServices(expectedServiceNames, timeout);

    scrapedServices.Should().Contain(
      expectedServiceNames,
      $"Prometheus should have scraped target_info metrics for [{string.Join(", ", expectedServiceNames)}] within {timeout}");
  }

  private async Task<ImmutableHashSet<string>> WaitForScrapedServices(
    ImmutableList<string> expectedServiceNames,
    TimeSpan timeout)
  {
    using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
    cts.CancelAfter(timeout);
    using var timer = new PeriodicTimer(500.Milliseconds());

    var latestScrapedServices = ImmutableHashSet<string>.Empty;

    try
    {
      do
      {
        try
        {
          latestScrapedServices = await QueryScrapedServiceNames(cts.Token);
          if (expectedServiceNames.All(s => latestScrapedServices.Contains(s)))
          {
            return latestScrapedServices;
          }
        }
        catch (OperationCanceledException) when (cts.IsCancellationRequested)
        {
          break;
        }
        catch
        {
          // Prometheus query may fail transiently while starting
        }
      } while (await timer.WaitForNextTickAsync(cts.Token));
    }
    catch (OperationCanceledException)
    {
    }

    return latestScrapedServices;
  }

  private async Task<ImmutableHashSet<string>> QueryScrapedServiceNames(CancellationToken ct)
  {
    var response = await prometheusClient
      .Request("api", "v1", "query")
      .SetQueryParams(new { query = "target_info" })
      .GetJsonAsync<JsonElement>(cancellationToken: ct);

    if (response.GetProperty("status").GetString() != "success")
    {
      return [];
    }

    var results = response.GetProperty("data").GetProperty("result");
    var services = ImmutableHashSet.CreateBuilder<string>(StringComparer.OrdinalIgnoreCase);

    foreach (var item in results.EnumerateArray())
    {
      if (item.TryGetProperty("metric", out var metric) &&
          metric.TryGetProperty("service_name", out var serviceNameElement) &&
          serviceNameElement.GetString() is { } serviceName)
      {
        services.Add(serviceName);
      }
    }

    return services.ToImmutable();
  }
}