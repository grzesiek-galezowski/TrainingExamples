using AwesomeAssertions;
using Flurl.Http;

namespace TrackClinicServiceSpecification.Automation;

public class MetricsResponse(IFlurlResponse response)
{
  public async Task ShouldBeSuccessful()
  {
    response.StatusCode.Should().Be(200);
    var content = await response.GetStringAsync();
    content.Should().NotBeNullOrWhiteSpace();
    // Basic Prometheus format check: typically starts with a comment or a metric name
    content.Should().MatchRegex("^(#|process_|dotnet_|http_).*");
  }
}
