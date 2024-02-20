using AuthorizationStructureExample.ProductionCode;

namespace AuthorizationStructureExample;

public class CheckingPrivilegesSpecification
{
  [Test]
  public void ShouldReturnTrueWhenGroupContainsADevice()
  {
    //GIVEN
    var s = new AuthorizationStructure(Any.Instance<IChangeEventsTarget>());
    //WHEN

    //THEN
    Assert.Fail("unfinished");
  }
}