namespace TrackClinicServiceSpecification;

[SetUpFixture, CancelAfter(60000)]
public class AssemblySetUp
{
  [OneTimeSetUp]
  public async Task SetUp()
  {
    await SharedMongoInstance.Start();
  }

  [OneTimeTearDown]
  public async Task TearDown()
  {
    await SharedMongoInstance.Stop();
  }
}