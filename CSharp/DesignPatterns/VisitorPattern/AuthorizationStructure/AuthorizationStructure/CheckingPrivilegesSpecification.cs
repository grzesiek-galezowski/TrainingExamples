using AuthorizationStructure.ProductionCode;

namespace AuthorizationStructure;

public class CheckingPrivilegesSpecification
{
  [Test]
  public void ShouldReturnTrueWhenGroupContainsADevice()
  {
    //GIVEN
    var s = new ProductionCode.AuthorizationStructure(Any.Instance<IChangeEventsTarget>());
    //WHEN

    //THEN
    Assert.Fail("unfinished");
  }
}