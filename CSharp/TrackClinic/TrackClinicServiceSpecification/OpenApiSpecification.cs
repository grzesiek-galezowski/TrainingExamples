using TrackClinicServiceSpecification.Automation;

namespace TrackClinicServiceSpecification;

//bug introduce a common timeout for all tests in the assembly
//bug the timeout does not seem to work
[CancelAfter(60000)] 
public class OpenApiSpecification
{
  [Test]
  public async Task ShouldBeValidAndAccessible(CancellationToken cancellationToken)
  {
    // GIVEN
    await using var driver = await TrackClinicDriver.CreateInstance(cancellationToken);
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
    await using var driver = await TrackClinicDriver.CreateInstance(cancellationToken);
    await driver.Start();

    // WHEN
    var webPageResponse = await driver.GetScalarUi();

    // THEN
    webPageResponse.ShouldHaveCorrectMetadata();
  }
}
