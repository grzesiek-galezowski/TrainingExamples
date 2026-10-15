using AwesomeAssertions;
using TrackClinic;

namespace TrackClinicServiceSpecification.Automation;

public class RetrieveProjectDetailsResponse(SongDetailsDto dto)
{
  
  public void ShouldBeEquivalentTo(SongDetailsDto songDetailsDto)
  {
    dto.Should().BeEquivalentTo(songDetailsDto);
  }
}