using DemoUiServiceSpecification.Automation;
using TddXt.AnyRoot;
using TddXt.AnyRoot.Strings;

namespace DemoUiServiceSpecification;

public class RunningScenariosSpecification
{
  [Test] // bug sometimes passes sometimes fails, needs to be investigated
  public async Task ShouldCallTrackClinicEndpointsWhenExecutingSingleScenario()
  {
    // GIVEN
    var artistName = Any.String();
    var song1Name = Any.String();
    var song2Name = Any.String();
    await using var driver = await DemoUiDriver.CreateInstance();
    driver.TrackClinic.SetupCreateSongToReturnSuccess(artistName);
    driver.TrackClinic.SetupGetSongDetailsToReturnSuccess(artistName, song1Name);
    driver.TrackClinic.SetupGetSongDetailsToReturnSuccess(artistName, song2Name);

    await driver.Start();

    // WHEN
    var result = await driver.Gui.RunSingleScenario(artistName, song1Name, song2Name);

    // THEN
    result.ShouldBeSuccess();
    driver.TrackClinic.ShouldReceiveRequestsInOrder(
    [
      ExpectedRequests.CreateSongRequest(artistName),
      ExpectedRequests.CreateSongRequest(artistName),
      ExpectedRequests.RetrieveSongDetailsRequest(artistName, song1Name),
      ExpectedRequests.RetrieveSongDetailsRequest(artistName, song2Name)
    ]);
  }

  [Test]
  public async Task ShouldCaptureFailureWhenTrackClinicReturnsError()
  {
    // GIVEN
    var artistName = Any.String();
    var song1Name = Any.String();
    var song2Name = Any.String();
    await using var driver = await DemoUiDriver.CreateInstance();

    driver.TrackClinic.SetupCreateSongToReturnSuccess(artistName);
    driver.TrackClinic.SetupGetSongDetailsToReturnBadRequest(artistName, song1Name);
    driver.TrackClinic.SetupGetSongDetailsToReturnBadRequest(artistName, song2Name);

    await driver.Start();

    // WHEN
    var result = await driver.Gui.RunSingleScenario(artistName, song1Name, song2Name);

    // THEN
    result.ShouldHaveCapturedBadRequestResponse();
  }
}
