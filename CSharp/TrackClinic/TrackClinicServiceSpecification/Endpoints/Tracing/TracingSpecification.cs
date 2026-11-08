using TddXt.AnyRoot;
using TddXt.AnyRoot.Strings;
using TrackClinic;
using TrackClinicServiceSpecification.Automation;

namespace TrackClinicServiceSpecification.Endpoints.Tracing;

public class TracingSpecification
{
  [Test]
  public async Task ShouldCaptureTraceForRequest()
  {
    // GIVEN
    var traceId = Any.TraceId();
    await using var driver = await TrackClinicDriver.CreateInstance();
    await driver.Start();

    // WHEN
    await driver.RequestCreatingSongProject(
      Any.String(),
      Any.Instance<CreateSongRequestDto>(),
      b => b.WithTraceId(traceId));

    // THEN
    await driver.Operations.ShouldHaveCapturedTrace(traceId, "POST", 201);
  }

  [Test]
  public async Task ShouldNotCaptureTraceForHealthCheckRequest()
  {
    // GIVEN
    var healthCheckTraceId = Any.TraceId();
    var createSongTraceId = Any.TraceId();
    await using var driver = await TrackClinicDriver.CreateInstance();
    await driver.Start();

    // bug metrics as well
    // WHEN
    await (await driver.RequestHealthCheck("/health/live", b => b.WithTraceId(healthCheckTraceId))).ShouldBeHealthy();
    // marker call to ensure that the health check trace can be queried
    await driver.CreateSongProject(Any.String(), Any.Instance<CreateSongRequestDto>(), b => b.WithTraceId(createSongTraceId));

    // THEN
    await driver.Operations.ShouldHaveCapturedTrace(createSongTraceId, "POST", 201);
    await driver.Operations.ShouldHaveNotCapturedTraceSoFar(healthCheckTraceId, "GET", 200);
  }
}
