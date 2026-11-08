using AwesomeAssertions;
using DemoUiServiceSpecification.Automation;

namespace DemoUiServiceSpecification.Endpoints.HomePage;

public class HomePageSpecification
{
  [Test]
  public async Task ShouldRenderMainDemoPage()
  {
    // GIVEN
    await using var driver = await DemoUiDriver.CreateInstance();
    await driver.Start();

    // WHEN
    var homePage = await driver.Gui.GetHomePageBodyText();

    // THEN
    homePage.Should().Contain("TrackClinic Demo");
    homePage.Should().Contain("Run Single Scenario");
    homePage.Should().Contain("Concurrent Traffic Generator");
  }
}
