using TddXt.AnyRoot;
using TddXt.AnyRoot.Strings;
using TrackClinicServiceSpecification.Automation;

namespace TrackClinicServiceSpecification.Endpoints.RetrieveSong;

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
    var response = await driver.RequestRetrievalOfProjectDetails(
      artistName,
      songName,
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
    var response = await driver.RequestRetrievalOfProjectDetails(
      artistName,
      songName,
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
    var response = await driver.RequestRetrievalOfProjectDetails(
      artistName,
      songName,
      r => r.WithInvalidToken());

    // THEN
    response.ShouldBeUnauthorized();
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
    var response = await driver.RequestRetrievalOfProjectDetails(
      artistName,
      songName,
      r => r.WithArtistUser());

    // THEN
    response.ShouldBeNotFound(); // a song does not exist, but the user is authorized to access
  }
}
