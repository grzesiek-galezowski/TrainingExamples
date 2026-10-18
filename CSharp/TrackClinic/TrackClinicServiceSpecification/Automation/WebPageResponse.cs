using AwesomeAssertions;
using Microsoft.Playwright;

namespace TrackClinicServiceSpecification.Automation;

public class WebPageResponse(IPage page)
{
  public async Task ShouldHaveCorrectMetadata()
  {
    var title = await page.TitleAsync();
    //bug
    title.Should().Contain("Scalar");

    var bodyText = await page.InnerTextAsync("body");
    //bug extract to constants:
    bodyText.Should().Contain("TrackClinic API");
    bodyText.Should().Contain("Creates a new song project");
    bodyText.Should().Contain("Gets details of a song project");
  }
}