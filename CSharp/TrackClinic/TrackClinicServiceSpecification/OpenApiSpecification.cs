using TrackClinicServiceSpecification.Automation;

namespace TrackClinicServiceSpecification;

[CancelAfter(60000)] //bug introduce a common timeout for all tests in the assembly
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
    response.ShouldHaveCorrectMetadata();
  }

  [Test]
  public async Task ShouldProvideScalarUi(CancellationToken cancellationToken)
  {
    // GIVEN
    await using var driver = new TrackClinicDriver(cancellationToken);
    await driver.Start();

    // WHEN
    var webPageResponse = await driver.GetScalarUi();

    // THEN
    await webPageResponse.ShouldHaveCorrectMetadata();
  }
}
