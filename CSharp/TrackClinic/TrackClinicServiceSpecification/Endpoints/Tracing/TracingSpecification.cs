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
  public async Task ShouldCaptureTraceForHealthCheckRequest()
  {
    // GIVEN
    var traceId = Any.TraceId();
    await using var driver = await TrackClinicDriver.CreateInstance();
    await driver.Start();

    // WHEN
    await driver.RequestHealthCheck("/health/live", b => b.WithTraceId(traceId));

    // THEN
    await driver.Operations.ShouldHaveCapturedTrace(traceId, "GET", 200);
  }
}
