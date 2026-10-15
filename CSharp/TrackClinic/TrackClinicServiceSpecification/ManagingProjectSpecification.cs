using TddXt.AnyRoot;
using TddXt.AnyRoot.Strings;
using TrackClinic;
using TrackClinicServiceSpecification.Automation;

namespace TrackClinicServiceSpecification;

[CancelAfter(30000)]
public class ManagingProjectSpecification
{
  [Test]
  public async Task ShouldCreateInitialSongWithNoProgress(CancellationToken cancellationToken) //bug
  {
    // GIVEN
    var artistName = Any.String();
    var songName1 = Any.String();
    var songName2 = Any.String();
    await using var driver = new TrackClinicDriver(cancellationToken);

    await driver.Start();
    await driver.CreateSongProject(artistName, new CreateSongRequestDto(songName1));
    await driver.CreateSongProject(artistName, new CreateSongRequestDto(songName2));

    // WHEN
    var response1 = await driver.RetrieveProjectDetails(artistName, songName1);
    var response2 = await driver.RetrieveProjectDetails(artistName, songName2);

    // THEN
    response1.ShouldBeEquivalentTo(SongDetailsDto.Initial(artistName, songName1));
    response2.ShouldBeEquivalentTo(SongDetailsDto.Initial(artistName, songName2));
  }
}

// Response DTOs (Read Side)