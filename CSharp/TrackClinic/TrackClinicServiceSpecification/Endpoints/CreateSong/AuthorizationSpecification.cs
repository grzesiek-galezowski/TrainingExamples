using TddXt.AnyRoot;
using TddXt.AnyRoot.Strings;
using AwesomeAssertions.Extensions;
using TrackClinic;
using TrackClinicServiceSpecification.Automation;

namespace TrackClinicServiceSpecification.Endpoints.CreateSong;

[CancelAfter(30000)]
public class AuthorizationSpecification
{
  [Test]
  public async Task ShouldDenyAccessIfUserDoesNotHaveArtistRole(CancellationToken cancellationToken)
  {
    // GIVEN
    var artistName = Any.String();
    var songName = Any.String();
    
    await using var driver = await TrackClinicDriver.CreateInstance(cancellationToken);

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
  public async Task ShouldDenyAccessIfNoTokenIsProvided(CancellationToken cancellationToken)
  {
    // GIVEN
    var artistName = Any.String();
    var songName = Any.String();
    
    await using var driver = await TrackClinicDriver.CreateInstance(cancellationToken);

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
  public async Task ShouldDenyAccessIfInvalidTokenIsProvided(CancellationToken cancellationToken)
  {
    // GIVEN
    var artistName = Any.String();
    var songName = Any.String();
    
    await using var driver = await TrackClinicDriver.CreateInstance(cancellationToken);

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
  public async Task ShouldDenyAccessIfTokenIsExpired(CancellationToken cancellationToken)
  {
    // GIVEN
    var artistName = Any.String();
    var songName = Any.String();
    
    await using var driver = await TrackClinicDriver.CreateInstance(cancellationToken);
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
  public async Task ShouldAllowAccessIfTokenIsExpiredButWithinGracePeriod(CancellationToken cancellationToken)
  {
    // GIVEN
    var artistName = Any.String();
    var songName = Any.String();
    
    await using var driver = await TrackClinicDriver.CreateInstance(cancellationToken);
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
  public async Task ShouldAllowAccessIfUserHasArtistRole(CancellationToken cancellationToken)
  {
    // GIVEN
    var artistName = Any.String();
    var songName = Any.String();
    
    await using var driver = await TrackClinicDriver.CreateInstance(cancellationToken);

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
