using System.Threading.Tasks;
using EndToEndSpecification.AutomationLayer;
using Xunit;
using static Lib.UserDtoBuilder;

namespace EndToEndSpecification
{
  public class E2ESpecification
  {
    [Fact]
    public async Task ShouldDOWHAT()
    {
      //GIVEN
      var johnny = AUser().Adult();
      using (var context = new AppDriver())
      {
        //WHEN
        var response = await context.Create(johnny);

        //THEN
        response.ShouldIndicateSuccessfulCreationOf(johnny);
      }
    }

    [Fact]
    public async Task ShouldDOWHAT2()
    {
      //GIVEN
      using (var context = new AppDriver())
      {
        var johnny = AUser().Adult();
        var kate = AUser().Adult();
        await context.Create(johnny);
        await context.Create(kate);

        //WHEN
        var probablyJohnny = await context.Get(johnny);
        var probablyKate = await context.Get(kate);

        //THEN
        probablyJohnny.ShouldBe(johnny);
        probablyKate.ShouldBe(kate);
      }
    }
  }
}