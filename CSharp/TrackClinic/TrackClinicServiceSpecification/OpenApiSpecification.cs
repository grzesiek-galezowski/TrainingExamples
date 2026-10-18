using TrackClinicServiceSpecification.Automation;
using AwesomeAssertions;

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
    response.ShouldHaveCorrectMetadata();
  }

  [Test]
  public async Task ShouldProvideScalarUi(CancellationToken cancellationToken)
  {
    // GIVEN
    await using var driver = new TrackClinicDriver(cancellationToken);
    await driver.Start();

    // WHEN
    var response = await driver.GetScalarUi();

    // THEN
    response.StatusCode.Should().Be(200);
    var content = await response.GetStringAsync();
    content.Should().Contain("<script");
    content.Should().Contain("scalar");
    //bug more advanced test that at least checks some endpoints
  }
}
