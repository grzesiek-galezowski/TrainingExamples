using AwesomeAssertions;
using Flurl.Http;

namespace ServiceSpecificationBuildingBlocks.Operations;

public class HealthResponse(IFlurlResponse response, string healthyString)
{
  public async Task ShouldBeHealthy()
  {
    response.StatusCode.Should().Be(200);
    (await response.GetStringAsync()).Should().Be(healthyString);
  }

  public async Task ShouldBeUnhealthy()
  {
    response.StatusCode.Should().Be(503);
    (await response.GetStringAsync()).Should().Be("Unhealthy");
  }

  public async Task<bool> IsHealthy()
  {
    return response.StatusCode == 200 && await response.GetStringAsync() == healthyString;
  }
  
  public async Task<string> GetStatusMessage()
  {
    return $"HealthResponse: {response.StatusCode} + {await response.GetStringAsync()}";
  }
}