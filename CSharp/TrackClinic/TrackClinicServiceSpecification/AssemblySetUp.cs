using TrackClinicServiceSpecification.Automation;
using TrackClinicServiceSpecification.Automation.Keycloak;

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
      SharedMongoInstance.Start(),
      SharedKeycloakInstance.Start()
    );
  }

  [OneTimeTearDown]
  public async Task TearDown()
  {
    await Task.WhenAll(
      SharedMongoInstance.Stop(),
      SharedKeycloakInstance.Stop()
    );
  }
}
