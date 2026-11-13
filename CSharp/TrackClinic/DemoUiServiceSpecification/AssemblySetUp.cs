using ServiceSpecificationBuildingBlocks.Browser;
using ServiceSpecificationBuildingBlocks.Keycloak;
using ServiceSpecificationBuildingBlocks.Zipkin;

#if !NCRUNCH
[assembly: Parallelizable(ParallelScope.All)]
[assembly: LevelOfParallelism(16)]
#endif

namespace DemoUiServiceSpecification;

[SetUpFixture]
public class AssemblySetUp
{
  [OneTimeSetUp]
  public async Task SetUp()
  {
    await Task.WhenAll(
      SharedPlaywrightInstance.Start(TestContext.CurrentContext.CancellationToken),
      SharedKeycloakInstance.Start(TestContext.CurrentContext.CancellationToken),
      SharedZipkinInstance.Start(TestContext.CurrentContext.CancellationToken));
  }

  [OneTimeTearDown]
  public async Task TearDown()
  {
    await Task.WhenAll(
      SharedKeycloakInstance.Stop(TestContext.CurrentContext.CancellationToken),
      SharedPlaywrightInstance.Stop(TestContext.CurrentContext.CancellationToken),
      SharedZipkinInstance.Stop(TestContext.CurrentContext.CancellationToken));
  }
}
