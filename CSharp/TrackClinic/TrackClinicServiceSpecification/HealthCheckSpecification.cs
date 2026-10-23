using TrackClinicServiceSpecification.Automation;

namespace TrackClinicServiceSpecification;

[CancelAfter(30000)]
public class HealthCheckSpecification
{
  //bug better tests here. Without the containers healthy should pass but ready should fail.
  [Test]
  public async Task ShouldBeHealthy(CancellationToken cancellationToken)
  {
    // GIVEN
    await using var driver = new TrackClinicDriver(cancellationToken);
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
    await using var driver = new TrackClinicDriver(cancellationToken);
    await driver.Start();

    // WHEN
    var readiness = await driver.RequestHealthCheck("/health/ready");

    // THEN
    await readiness.ShouldBeHealthy();
  }
}
