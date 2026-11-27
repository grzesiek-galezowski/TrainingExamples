using AwesomeAssertions;

namespace DemoUiServiceSpecification.Automation;

public class SingleScenarioUiTestResponse(string content)
{
  private const string SucceededText = "✔ Succeeded";
  private const string FailedText = "✖ Failed";

  public void ShouldHaveCapturedBadRequestResponse()
  {
    content.Should().Contain(FailedText);
    content.Should().NotContain(SucceededText);
    content.Should().Contain("400");
  }

  public void ShouldBeSuccess()
  {
    content.Should().Contain(SucceededText);
    content.Should().NotContain(FailedText);
  }
}