using AwesomeAssertions;
using Flurl.Http;

namespace TrackClinicServiceSpecification.Automation;

public class HealthResponse(IFlurlResponse response)
{
  public async Task ShouldBeHealthy()
  {
    response.StatusCode.Should().Be(200);
    (await response.GetStringAsync()).Should().Be("Healthy");
  }

  public async Task<bool> IsHealthy() => response.StatusCode == 200 && await response.GetStringAsync() == "Healthy";
}