using System.Text.Json;
using AwesomeAssertions;

namespace TrackClinicServiceSpecification.Automation;

public class OpenApiDocumentResponse(string document)
{
  public void ShouldBeValidJson()
  {
    document.Should().NotBeNullOrWhiteSpace();
    using var json = JsonDocument.Parse(document);
    json.RootElement.GetProperty("openapi").GetString().Should().StartWith("3.");
  }

  public void ShouldContainTheApplicationPaths()
  {
    document.Should().Contain("/api/v1/artists/{artistName}/songs");
    document.Should().Contain("/api/v1/artists/{artistName}/songs/{songName}");
  }

  public void ShouldHaveCorrectMetadata()
  {
    using var json = JsonDocument.Parse(document);
    var info = json.RootElement.GetProperty("info");
    info.GetProperty("title").GetString().Should().Be("TrackClinic API");
    info.GetProperty("version").GetString().Should().Be("v1");
    info.GetProperty("description").GetString().Should().Be("API for managing song projects and artist collaborations.");
    var contact = info.GetProperty("contact");
    contact.GetProperty("name").GetString().Should().Be("TrackClinic Support");
    contact.GetProperty("email").GetString().Should().Be("support@trackclinic.example.com");
  }
}