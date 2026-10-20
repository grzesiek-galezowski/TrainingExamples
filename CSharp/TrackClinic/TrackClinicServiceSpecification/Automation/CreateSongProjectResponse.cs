using AwesomeAssertions;
using Flurl.Http;

namespace TrackClinicServiceSpecification.Automation;

public class CreateSongProjectResponse(IFlurlResponse flurlResponse)
{
  public void ShouldBeSuccess()
  {
    flurlResponse.StatusCode.Should().Be(201);
  }

  public void ShouldBeForbidden()
  {
    flurlResponse.StatusCode.Should().Be(403);
  }
}