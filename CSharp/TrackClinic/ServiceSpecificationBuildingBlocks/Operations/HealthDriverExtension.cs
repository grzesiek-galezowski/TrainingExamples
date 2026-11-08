using Flurl.Http;

namespace ServiceSpecificationBuildingBlocks.Operations;

public class HealthDriverExtension(FlurlClient client, CancellationToken cancellationToken)
{
  public async Task<HealthResponse> RequestHealthCheck(string urlSegment)
  {
    return new HealthResponse(
      await client.Request(urlSegment).GetAsync(cancellationToken: cancellationToken));
  }

  public async Task WaitForHealthCheck(string urlSegment, TimeSpan timeout)
  {
    using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
    cts.CancelAfter(timeout);
    using var timer = new PeriodicTimer(TimeSpan.FromMilliseconds(100));
    while (await timer.WaitForNextTickAsync(cts.Token))
    {
      if (await (await RequestHealthCheck(urlSegment)).IsHealthy())
      {
        return;
      }
    }
  }
}