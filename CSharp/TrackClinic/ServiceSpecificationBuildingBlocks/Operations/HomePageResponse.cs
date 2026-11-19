using AwesomeAssertions;
using Flurl.Http;

namespace ServiceSpecificationBuildingBlocks.Operations;

public class HomePageResponse(IFlurlResponse response)
{
  public async Task ShouldBeSuccessful()
  {
    response.StatusCode.Should().Be(200);
    var content = await response.GetStringAsync();
    content.Should().NotBeNullOrWhiteSpace();
  }

  public async Task ShouldContain(string expectedText)
  {
    var content = await response.GetStringAsync();
    content.Should().Contain(expectedText);
  }
}
