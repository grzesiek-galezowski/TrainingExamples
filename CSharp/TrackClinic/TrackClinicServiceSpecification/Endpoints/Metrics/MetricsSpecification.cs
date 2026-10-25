using TrackClinicServiceSpecification.Automation;

namespace TrackClinicServiceSpecification.Endpoints.Metrics;

public class MetricsSpecification
{
  [Test]
  public async Task ShouldProvidePrometheusMetrics()
  {
    // GIVEN
    await using var driver = await TrackClinicDriver.CreateInstance();
    await driver.Start();

    // WHEN
    var response = await driver.GetMetrics();

    // THEN
    await response.ShouldBeSuccessful();
  }
}
