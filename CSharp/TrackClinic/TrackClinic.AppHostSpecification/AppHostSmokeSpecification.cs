using TddXt.AnyRoot;
using TddXt.AnyRoot.Strings;
using TrackClinic.AppHostSpecification.Automation;

namespace TrackClinic.AppHostSpecification;

public class AppHostSmokeSpecification
{
  [Test]
  public async Task ShouldExecuteSingleScenarioAndReportTelemetryAcrossServices()
  {
    // GIVEN
    var artistName = Any.String();
    var song1Name = Any.String();
    var song2Name = Any.String();
    await using var driver = await AppHostDriver.CreateInstance();
    await driver.Start();
    await AssertAllServicesAreHealthy(driver);

    // WHEN
    var scenarioResult = await driver.Gui.RunSingleScenario(artistName, song1Name, song2Name);

    // THEN
    scenarioResult.ShouldBeSuccess();
    await driver.Zipkin.ShouldHaveCapturedScenarioTrace(artistName, song1Name, song2Name);
    await driver.Prometheus.ShouldHaveScrapedMetricsFor(["DemoUi", "TrackClinic"]);
    await driver.Grafana.ShouldHaveProvisionedDatasources(["Prometheus", "Zipkin"]);
    await driver.Grafana.ShouldHaveProvisionedDashboards(
      ["Track Clinic - Service Overview", "Track Clinic - Zipkin Traces"]);
  }

  private static async Task AssertAllServicesAreHealthy(AppHostDriver driver)
  {
    await (await driver.DemoUiHealth.RequestHealthCheck("/health/ready")).ShouldBeHealthy();
    await (await driver.TrackClinicHealth.RequestHealthCheck("/health/ready")).ShouldBeHealthy();
    await (await driver.ZipkinHealthCheck.RequestHealthCheck("health")).ShouldBeHealthy();
    await (await driver.PrometheusHealth.RequestHealthCheck("/-/healthy")).ShouldBeHealthy();
    await (await driver.GrafanaHealthCheck.RequestHealthCheck("/api/health")).ShouldBeHealthy();
  }
}