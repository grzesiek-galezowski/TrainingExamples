using AwesomeAssertions.Extensions;
using TddXt.AnyRoot;
using TddXt.AnyRoot.Strings;
using TrackClinicServiceSpecification.Automation;

namespace TrackClinicServiceSpecification.Endpoints.RetrieveSong;

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
    var response = await driver.RequestRetrievalOfProjectDetails(
      artistName,
      songName,
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
    var response = await driver.RequestRetrievalOfProjectDetails(
      artistName,
      songName,
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
    var response = await driver.RequestRetrievalOfProjectDetails(
      artistName,
      songName,
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
    var response = await driver.RequestRetrievalOfProjectDetails(
      artistName,
      songName,
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
    var response = await driver.RequestRetrievalOfProjectDetails(
      artistName,
      songName,
      r => r.WithArtistUser().WithDelayBeforeSending(2.Seconds()));

    // THEN
    // Note: since the song does not exist, it might be 404, but it shouldn't be 401/403
    response.ShouldBeNotFound();
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
    var response = await driver.RequestRetrievalOfProjectDetails(
      artistName,
      songName,
      r => r.WithArtistUser());

    // THEN
    response.ShouldBeNotFound(); // a song does not exist, but the user is authorized to access
  }
}
