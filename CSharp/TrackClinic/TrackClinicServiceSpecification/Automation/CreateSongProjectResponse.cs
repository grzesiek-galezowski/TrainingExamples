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

  public void ShouldBeUnauthorized()
  {
    flurlResponse.StatusCode.Should().Be(401);
  }

  public void ShouldContainLinkTo(string artistName, string songName)
  {
    flurlResponse.Headers.Should().ContainSingle(tuple =>
      tuple.Name == "Location" && tuple.Value.Contains($"/api/v1/artists/{artistName}/songs/{songName}"));
  }
}