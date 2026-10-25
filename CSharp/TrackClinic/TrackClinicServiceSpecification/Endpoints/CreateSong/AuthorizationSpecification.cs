using TddXt.AnyRoot;
using TddXt.AnyRoot.Strings;
using AwesomeAssertions.Extensions;
using TrackClinic;
using TrackClinicServiceSpecification.Automation;

namespace TrackClinicServiceSpecification.Endpoints.CreateSong;

public class AuthorizationSpecification
{
  [Test]
  public async Task ShouldDenyAccessIfUserDoesNotHaveArtistRole()
  {
    // GIVEN
    var artistName = Any.String();
    var songName = Any.String();
    
    await using var driver = await TrackClinicDriver.CreateInstance();

    await driver.Start();

    // WHEN
    var response = await driver.RequestCreatingSongProject(
      artistName,
      new CreateSongRequestDto(songName),
      r => r.WithUnprivilegedUser());

    // THEN
    response.ShouldBeForbidden();
  }

  [Test]
  public async Task ShouldDenyAccessIfNoTokenIsProvided()
  {
    // GIVEN
    var artistName = Any.String();
    var songName = Any.String();
    
    await using var driver = await TrackClinicDriver.CreateInstance();

    await driver.Start();

    // WHEN
    var response = await driver.RequestCreatingSongProject(
      artistName,
      new CreateSongRequestDto(songName),
      r => r.WithoutToken());

    // THEN
    response.ShouldBeUnauthorized();
  }

  [Test]
  public async Task ShouldDenyAccessIfInvalidTokenIsProvided()
  {
    // GIVEN
    var artistName = Any.String();
    var songName = Any.String();
    
    await using var driver = await TrackClinicDriver.CreateInstance();

    await driver.Start();

    // WHEN
    var response = await driver.RequestCreatingSongProject(
      artistName,
      new CreateSongRequestDto(songName),
      r => r.WithInvalidToken());

    // THEN
    response.ShouldBeUnauthorized();
  }

  [Test]
  public async Task ShouldDenyAccessIfTokenIsExpired()
  {
    // GIVEN
    var artistName = Any.String();
    var songName = Any.String();
    
    await using var driver = await TrackClinicDriver.CreateInstance();
    driver.SetupKeycloakTokenLifespan(1.Seconds());
    driver.SetupGracePeriod(TimeSpan.Zero);
    await driver.Start();

    // WHEN
    var response = await driver.RequestCreatingSongProject(
      artistName,
      new CreateSongRequestDto(songName),
      r => r.WithArtistUser().WithDelayBeforeSending(2.Seconds()));

    // THEN
    response.ShouldBeUnauthorized();
  }

  [Test]
  public async Task ShouldAllowAccessIfTokenIsExpiredButWithinGracePeriod()
  {
    // GIVEN
    var artistName = Any.String();
    var songName = Any.String();
    
    await using var driver = await TrackClinicDriver.CreateInstance();
    driver.SetupKeycloakTokenLifespan(1.Seconds());
    await driver.Start();

    // WHEN
    var response = await driver.RequestCreatingSongProject(
      artistName,
      new CreateSongRequestDto(songName),
      r => r.WithArtistUser().WithDelayBeforeSending(2.Seconds()));

    // THEN
    response.ShouldBeSuccess();
  }

  [Test]
  public async Task ShouldAllowAccessIfUserHasArtistRole()
  {
    // GIVEN
    var artistName = Any.String();
    var songName = Any.String();
    
    await using var driver = await TrackClinicDriver.CreateInstance();

    await driver.Start();

    // WHEN
    var response = await driver.RequestCreatingSongProject(
      artistName,
      new CreateSongRequestDto(songName),
      r => r.WithArtistUser());

    // THEN
    response.ShouldBeSuccess();
  }
}
