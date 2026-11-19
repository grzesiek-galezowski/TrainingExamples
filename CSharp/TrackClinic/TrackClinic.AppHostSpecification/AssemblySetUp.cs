using ServiceSpecificationBuildingBlocks.Browser;

namespace TrackClinic.AppHostSpecification;

[SetUpFixture]
public class AssemblySetUp
{
  [OneTimeSetUp]
  public async Task SetUp()
  {
    await SharedPlaywrightInstance.Start(TestContext.CurrentContext.CancellationToken);
  }

  [OneTimeTearDown]
  public async Task TearDown()
  {
    await SharedPlaywrightInstance.Stop(TestContext.CurrentContext.CancellationToken);
  }
}
