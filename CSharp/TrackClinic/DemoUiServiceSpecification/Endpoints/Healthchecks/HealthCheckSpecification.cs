using DemoUiServiceSpecification.Automation;

namespace DemoUiServiceSpecification.Endpoints.Healthchecks;

public class HealthCheckSpecification
{
  [Test]
  public async Task ShouldBeHealthy()
  {
    // GIVEN
    await using var driver = await DemoUiDriver.CreateInstance();
    await driver.Start();

    // WHEN
    var live = await driver.Health.RequestHealthCheck("/health/live");
    var ready = await driver.Health.RequestHealthCheck("/health/ready");

    // THEN
    await live.ShouldBeHealthy();
    await ready.ShouldBeHealthy();
  }
}
