using TrackClinicServiceSpecification.Automation;

namespace TrackClinicServiceSpecification.Endpoints.Healthchecks;

public class HealthCheckSpecification
{
  [Test]
  public async Task ShouldBeHealthy()
  {
    // GIVEN
    await using var driver = await TrackClinicDriver.CreateInstance();
    await driver.Start();

    // WHEN
    var health = await driver.RequestHealthCheck("/health/live");

    // THEN
    await health.ShouldBeHealthy();
  }

  [Test]
  public async Task ShouldBeReady()
  {
    // GIVEN
    await using var driver = await TrackClinicDriver.CreateInstance();
    await driver.Start();

    // WHEN
    var readiness = await driver.RequestHealthCheck("/health/ready");

    // THEN
    await readiness.ShouldBeHealthy();
  }

  [Test]
  public async Task ShouldReportNotReadyWhenMongoIsUnavailable()
  {
    // GIVEN
    await using var driver = await TrackClinicDriver.CreateInstance();
    await driver.Start();

    driver.SimulateMongoFailure();

    // WHEN
    var live = await driver.RequestHealthCheck("/health/live");
    var ready = await driver.RequestHealthCheck("/health/ready");

    // THEN
    await live.ShouldBeHealthy();
    await ready.ShouldBeUnhealthy();
  }
}
