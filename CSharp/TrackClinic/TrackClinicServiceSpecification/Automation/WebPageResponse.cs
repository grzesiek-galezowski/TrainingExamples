using AwesomeAssertions;
namespace TrackClinicServiceSpecification.Automation;

public class WebPageResponse(string title, string bodyText)
{
  public void ShouldHaveCorrectMetadata()
  {
    title.Should().Contain("Scalar");

    bodyText.Should().Contain("TrackClinic API");
    bodyText.Should().Contain("Creates a new song project");
    bodyText.Should().Contain("Gets details of a song project");
  }
}