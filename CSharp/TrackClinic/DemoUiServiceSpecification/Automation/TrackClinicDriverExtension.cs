using System.Collections.Immutable;
using AwesomeAssertions;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace DemoUiServiceSpecification.Automation;

public class TrackClinicDriverExtension(WireMockServer trackClinicMock, int priority)
{
  public void SetupGetSongDetailsToReturnSuccess(string artist, string songName)
  {
    var path = $"/api/v1/artists/{Uri.EscapeDataString(artist)}/songs/{Uri.EscapeDataString(songName)}";
    trackClinicMock
      .Given(Request.Create().WithPath(path).UsingGet())
      .AtPriority(priority)
      .RespondWith(Response.Create().WithStatusCode(200).WithBody("details"));
  }

  public void SetupCreateSongToReturnSuccess(string artist)
  {
    var path = $"/api/v1/artists/{Uri.EscapeDataString(artist)}/songs";
    trackClinicMock //bug add priorities
      .Given(Request.Create().WithPath(path).UsingPost())
      .AtPriority(priority)
      .RespondWith(Response.Create().WithStatusCode(201).WithBody("created"));
  }

  public void SetupGetSongDetailsToReturnBadRequest(string artist, string songName)
  {
    var path = $"/api/v1/artists/{Uri.EscapeDataString(artist)}/songs/{Uri.EscapeDataString(songName)}";
    trackClinicMock
      .Given(Request.Create().WithPath(path).UsingGet())
      .AtPriority(priority)
      .RespondWith(Response.Create().WithStatusCode(400).WithBody("bad request"));
  }

  private IReadOnlyList<string> TrackClinicRequestPaths() =>
    trackClinicMock.LogEntries.Select(x => x.RequestMessage!.Path)
      .ToImmutableList(); //bug enable warnings as errors nullable

  public void ShouldReceiveRequestsInOrder(ImmutableList<string> expectation)
  {
    TrackClinicRequestPaths().Should().Equal(expectation);
  }
}