using System.Text.Json;
using AwesomeAssertions;

namespace TrackClinicServiceSpecification.Automation;

public class OpenApiDocumentResponse(string document)
{
  public void ShouldBeValidJson()
  {
    document.Should().NotBeNullOrWhiteSpace();
    var json = JsonDocument.Parse(document);
    json.RootElement.GetProperty("openapi").GetString().Should().StartWith("3.");
  }

  public void ShouldContainTheApplicationPaths()
  {
    document.Should().Contain("/api/v1/artists/{artistName}/songs");
    document.Should().Contain("/api/v1/artists/{artistName}/songs/{songName}");
  }
}