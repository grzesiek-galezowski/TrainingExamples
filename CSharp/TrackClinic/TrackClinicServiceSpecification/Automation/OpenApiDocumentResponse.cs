using System.Text.Json;
using AwesomeAssertions;
using TrackClinic;

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
    info.GetProperty("title").GetString().Should().Be(OpenApiConstants.Title);
    info.GetProperty("version").GetString().Should().Be(OpenApiConstants.Version);
    info.GetProperty("description").GetString().Should().Be(OpenApiConstants.Description);
    var contact = info.GetProperty("contact");
    contact.GetProperty("name").GetString().Should().Be(OpenApiConstants.ContactName);
    contact.GetProperty("email").GetString().Should().Be(OpenApiConstants.ContactEmail);
  }
}