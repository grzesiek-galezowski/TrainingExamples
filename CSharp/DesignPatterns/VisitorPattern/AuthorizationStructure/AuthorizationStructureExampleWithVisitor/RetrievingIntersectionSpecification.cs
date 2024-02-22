using AuthorizationStructureExampleWithVisitor.ProductionCode;
using FluentAssertions;
using static AuthorizationStructureExampleWithVisitor.ProductionCode.AuthorizationStructure;

namespace AuthorizationStructureExampleWithVisitor;

internal class RetrievingIntersectionSpecification
{
  [Test]
  public void ShouldWHAT()
  {
    //GIVEN
    var s = new AuthorizationStructure(Any.Instance<IChangeEventsTarget>());
    var dev1 = Any.String();
    var dev2 = Any.String();
    var dev3 = Any.String();
    var dev4 = Any.String();
    var user1 = Any.String();
    var userGroup = Any.String();
    
    s.AddDevice(RootNodeId.Name, dev1, Any.String());
    s.AddGroup(RootNodeId.Name, userGroup);
    s.AddUser(userGroup, user1);
    s.AddDevice(userGroup, dev2, Any.String());
    s.AddDevice(userGroup, dev3, Any.String());
    s.AddDevice(userGroup, dev4, Any.String());

    //WHEN
    var intersection = s.RetrieveIdsOfDevicesOwnedUserFromAmong(
      LanguageExt.Seq.createRange([dev1, dev2, dev3]), 
      user1);

    //THEN
    intersection.Should().Equal(LanguageExt.HashSet.createRange([NodeId.Device(dev2), NodeId.Device(dev3)]));
  }
}