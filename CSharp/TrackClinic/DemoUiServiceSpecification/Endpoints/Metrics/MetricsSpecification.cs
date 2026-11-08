using AwesomeAssertions;
using DemoUiServiceSpecification.Automation;

namespace DemoUiServiceSpecification.Endpoints.Metrics;

public class MetricsSpecification
{
  [Test] //bug more tests for metrics
  public async Task ShouldProvidePrometheusMetrics()
  {
    // GIVEN
    await using var driver = await DemoUiDriver.CreateInstance();
    await driver.Start();

    // WHEN
    var metrics = await driver.RequestMetrics();

    // THEN
    metrics.Should().Contain("# HELP");
    metrics.Should().Contain("# TYPE");
  }
}
