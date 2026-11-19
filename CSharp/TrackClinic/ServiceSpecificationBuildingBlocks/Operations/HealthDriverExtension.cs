using Flurl.Http;
using NUnit.Framework;

namespace ServiceSpecificationBuildingBlocks.Operations;

public class HealthDriverExtension(FlurlClient client, string healthyString, CancellationToken cancellationToken)
{
  public async Task<HealthResponse> RequestHealthCheck(string urlSegment)
  {
    return new HealthResponse(
      await client.Request(urlSegment).AllowAnyHttpStatus().GetAsync(cancellationToken: cancellationToken),
      healthyString);
  }

  public async Task WaitForHealthCheck(string urlSegment, TimeSpan timeout)
  {
    using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
    cts.CancelAfter(timeout);
    using var timer = new PeriodicTimer(TimeSpan.FromMilliseconds(100));
    while (await timer.WaitForNextTickAsync(cts.Token))
    {
      try
      {
        var healthResponse = await RequestHealthCheck(urlSegment);
        if (await healthResponse.IsHealthy())
        {
          return;
        }
        else
        {
          TestContext.WriteLine(
            $"Health check for '{urlSegment}' returned status \"{await healthResponse.GetStatusMessage()}\". Expected: \"{healthyString}\". Retrying...");
        }
      }
      catch (Exception ex)
      {
        TestContext.WriteLine($"Health check for '{urlSegment}' failed: {ex.Message}. Retrying...");
      }
    }
  }
}