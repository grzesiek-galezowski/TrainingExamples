using AuthorizationStructure.ProductionCode;
using FluentAssertions;

namespace AuthorizationStructure;

public class RetrievingIdsSpecification
{
  private NodeId RootId => ProductionCode.AuthorizationStructure.RootNodeId;

  [Test]
  public void ShouldAllowGettingAllDevicesForUserFromTheSameLevel()
  {
    //GIVEN
    var dev1 = Any.String();
    var dev2 = Any.String();
    var user1 = Any.String();
    var s = new ProductionCode.AuthorizationStructure();
    s.AddDevice(RootId, dev1);
    s.AddDevice(RootId, dev2);
    s.AddUser(RootId, user1);

    //WHEN
    var devices = s.ResolveUserIdIntoDevices(user1);

    //THEN
    devices.Should().Equal(NodeId.Device(dev1), NodeId.Device(dev2));
  }
}