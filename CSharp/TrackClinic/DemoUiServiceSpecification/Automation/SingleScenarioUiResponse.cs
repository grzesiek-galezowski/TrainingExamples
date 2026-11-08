using AwesomeAssertions;

namespace DemoUiServiceSpecification.Automation;

//bug what is this???? O_O
public record SingleScenarioUiResponse(bool IsSuccess, bool IsFailure, string BodyText)
{
  public void ShouldHaveCapturedBadRequestResponse()
  {
    IsFailure.Should().BeTrue();
    IsSuccess.Should().BeFalse();
    BodyText.Should().Contain("400");
  }

  public void ShouldBeSuccess()
  {
    IsSuccess.Should().BeTrue();
    IsFailure.Should().BeFalse();
  }
}