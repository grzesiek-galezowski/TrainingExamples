using TrackClinicServiceSpecification.Automation;

namespace TrackClinicServiceSpecification;

[CancelAfter(30000)]
public class HealthCheckSpecification
{
  [Test]
  public async Task ShouldBeHealthy(CancellationToken cancellationToken)
  {
    // GIVEN
    await using var driver = await TrackClinicDriver.CreateInstance(cancellationToken);
    await driver.Start();

    // WHEN
    var health = await driver.RequestHealthCheck("/health/live");

    // THEN
    await health.ShouldBeHealthy();
  }

  [Test]
  public async Task ShouldBeReady(CancellationToken cancellationToken)
  {
    // GIVEN
    await using var driver = await TrackClinicDriver.CreateInstance(cancellationToken);
    await driver.Start();

    // WHEN
    var readiness = await driver.RequestHealthCheck("/health/ready");

    // THEN
    await readiness.ShouldBeHealthy();
  }

  [Test]
  public async Task ShouldReportNotReadyWhenMongoIsUnavailable(CancellationToken cancellationToken)
  {
    // GIVEN
    await using var driver = await TrackClinicDriver.CreateInstance(cancellationToken);
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
