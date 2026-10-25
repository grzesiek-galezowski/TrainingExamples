using TddXt.AnyRoot;
using TddXt.AnyRoot.Strings;
using TrackClinic;
using TrackClinicServiceSpecification.Automation;

namespace TrackClinicServiceSpecification;

public class ManagingProjectSpecification
{
  [Test]
  public async Task ShouldRespondWithSuccessAndSongLinkToCreatingNewSong()
  {
    // GIVEN
    var artistName = Any.String();
    var songName = Any.String();
    await using var driver = await TrackClinicDriver.CreateInstance();

    await driver.Start();
    
    // WHEN
    var response = await driver.RequestCreatingSongProject(artistName, new CreateSongRequestDto(songName));

    // THEN
    response.ShouldBeSuccess();
    response.ShouldContainLinkTo(artistName, songName);
  }

  [Test]
  public async Task ShouldRespondWithNotFoundWhenRequestingNonExistentSong()
  {
    // GIVEN
    var artistName = Any.String();
    var songName = Any.String();
    await using var driver = await TrackClinicDriver.CreateInstance();

    await driver.Start();

    // WHEN
    var response = await driver.RequestRetrievalOfProjectDetails(artistName, songName);

    // THEN
    response.ShouldBeNotFound();
  }


  [Test]
  public async Task ShouldCreateInitialSongWithNoProgress()
  {
    // GIVEN
    var artistName = Any.String();
    var songName1 = Any.String();
    var songName2 = Any.String();
    await using var driver = await TrackClinicDriver.CreateInstance();

    await driver.Start();
    await driver.CreateSongProject(artistName, new CreateSongRequestDto(songName1));
    await driver.CreateSongProject(artistName, new CreateSongRequestDto(songName2));

    // WHEN
    var response1 = await driver.RetrieveProjectDetails(artistName, songName1);
    var response2 = await driver.RetrieveProjectDetails(artistName, songName2);

    // THEN
    await response1.ShouldHaveContentEquivalentTo(SongDetailsDto.Initial(artistName, songName1));
    await response2.ShouldHaveContentEquivalentTo(SongDetailsDto.Initial(artistName, songName2));
  }
}

//bug validation tests + functionality