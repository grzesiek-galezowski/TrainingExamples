namespace TrackClinicServiceSpecification;

[SetUpFixture, CancelAfter(120000)]
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