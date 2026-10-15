using TrackClinicServiceSpecification.Automation;

namespace TrackClinicServiceSpecification;

[CancelAfter(30000)]
public class OpenApiSpecification
{
  [Test]
  public async Task ShouldBeValidAndAccessible(CancellationToken cancellationToken)
  {
    // GIVEN
    await using var driver = new TrackClinicDriver(cancellationToken);
    await driver.Start();

    // WHEN
    var response = await driver.GetOpenApiDocument();

    // THEN
    response.ShouldBeValidJson();
    response.ShouldContainTheApplicationPaths();
  }
}
