using TrackClinicServiceSpecification.Automation;

namespace TrackClinicServiceSpecification.Endpoints.OpenApi;

public class OpenApiSpecification
{
  [Test]
  public async Task ShouldBeValidAndAccessible()
  {
    // GIVEN
    await using var driver = await TrackClinicDriver.CreateInstance();
    await driver.Start();

    // WHEN
    var response = await driver.Operations.GetOpenApiDocument();

    // THEN
    response.ShouldBeValidJson();
    response.ShouldContainTheApplicationPaths();
    response.ShouldHaveCorrectMetadata();
  }

  [Test]
  public async Task ShouldProvideScalarUi()
  {
    // GIVEN
    await using var driver = await TrackClinicDriver.CreateInstance();
    await driver.Start();

    // WHEN
    var webPageResponse = await driver.Operations.GetScalarUi();

    // THEN
    webPageResponse.ShouldHaveCorrectMetadata();
  }
}
