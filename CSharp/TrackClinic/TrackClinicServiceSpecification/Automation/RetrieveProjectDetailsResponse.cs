using AwesomeAssertions;
using Flurl.Http;
using TrackClinic;

namespace TrackClinicServiceSpecification.Automation;

public class RetrieveProjectDetailsResponse(IFlurlResponse response)
{
  
  public async Task ShouldHaveContentEquivalentTo(SongDetailsDto songDetailsDto)
  {
    (await response.GetJsonAsync<SongDetailsDto>()).Should().BeEquivalentTo(songDetailsDto);
  }

  public void ShouldBeNotFound()
  {
    response.StatusCode.Should().Be(404);
  }

  public void ShouldBeSuccess()
  {
    response.StatusCode.Should().Be(200);
  }

  public void ShouldBeForbidden()
  {
    response.StatusCode.Should().Be(403);
  }

  public void ShouldBeUnauthorized()
  {
    response.StatusCode.Should().Be(401);
  }
}