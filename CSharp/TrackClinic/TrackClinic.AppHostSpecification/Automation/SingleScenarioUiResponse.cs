using AwesomeAssertions;

namespace TrackClinic.AppHostSpecification.Automation;

public class SingleScenarioUiResponse(string bodyText)
{
  public void ShouldBeSuccess()
  {
    bodyText.Should().Contain("✔ Succeeded");
    bodyText.Should().NotContain("✖ Failed");
  }

  public void ShouldHaveCapturedBadRequestResponse()
  {
    bodyText.Should().Contain("✖ Failed");
    bodyText.Should().NotContain("✔ Succeeded");
    bodyText.Should().Contain("400");
  }
}
