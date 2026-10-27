using TrackClinicServiceSpecification.Automation;
using TrackClinicServiceSpecification.Automation.Keycloak;
using TrackClinicServiceSpecification.Automation.Zipkin;

#if !NCRUNCH
[assembly: Parallelizable(ParallelScope.Children)]
[assembly: LevelOfParallelism(32)]
#endif

namespace TrackClinicServiceSpecification;

[SetUpFixture]
public class AssemblySetUp
{
  [OneTimeSetUp]
  public async Task SetUp()
  {
    await Task.WhenAll(
      SharedMongoInstance.Start(TestContext.CurrentContext.CancellationToken),
      SharedKeycloakInstance.Start(TestContext.CurrentContext.CancellationToken),
      SharedZipkinInstance.Start(TestContext.CurrentContext.CancellationToken)
    );
  }

  [OneTimeTearDown]
  public async Task TearDown()
  {
    await Task.WhenAll(
      SharedMongoInstance.Stop(TestContext.CurrentContext.CancellationToken),
      SharedKeycloakInstance.Stop(TestContext.CurrentContext.CancellationToken),
      SharedZipkinInstance.Stop(TestContext.CurrentContext.CancellationToken)
    );
  }
}
