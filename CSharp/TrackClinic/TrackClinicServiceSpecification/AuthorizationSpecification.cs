using TddXt.AnyRoot;
using TddXt.AnyRoot.Strings;
using TrackClinic;
using TrackClinicServiceSpecification.Automation;

namespace TrackClinicServiceSpecification;

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

  //bug what about 401?? What about no token test? What about expired token test? What about invalid token test? What about token with no roles test?
}
