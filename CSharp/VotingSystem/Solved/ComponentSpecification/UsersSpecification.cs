using ComponentSpecification.AutomationLayer;
using Lib;
using Xunit;

namespace ComponentSpecification
{
  public class UsersSpecification
  {
    [Fact]
    public void ShouldDOWHAT()
    {
      //GIVEN
      var context = new ComponentDriver();
      context.Start();
      var johnny = UserDtoBuilder.AUser().Adult();

      //WHEN
      context.Create(johnny);

      //THEN
      context.ShouldReportSuccessfulCreationOf(johnny);
    }
    
    [Fact]
    public void ShouldDOWHAT2()
    {
      //GIVEN
      var context = new ComponentDriver();
      context.Start();
      var johnny = UserDtoBuilder.AUser().Adult();
      var kate = UserDtoBuilder.AUser().Adult();

      context.Create(johnny);
      context.Create(kate);

      //WHEN
      var probablyJohnny = context.Get(johnny);
      var probablyKate = context.Get(kate);

      //THEN
      probablyJohnny.ShouldBe(johnny);
      probablyKate.ShouldBe(kate);
    }
  }
}
