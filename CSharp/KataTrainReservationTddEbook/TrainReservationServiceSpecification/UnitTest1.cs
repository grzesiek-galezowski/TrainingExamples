using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using TrainReservationService;

namespace TrainReservationServiceSpecification;

public class Tests
{
  [Test]
  public void Test1()
  {
    using var webApp = new WebApplicationFactory<Program>().WithWebHostBuilder(b =>
    {
      b.ConfigureTestServices(ctx =>
      {

      });
    });

    
  }
}