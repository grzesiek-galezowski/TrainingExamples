using AuthorizationStructure.ProductionCode;
using FluentAssertions;
using LanguageExt;
using static AuthorizationStructure.ProductionCode.AuthorizationStructure;

namespace AuthorizationStructure;

public class RetrievingIdsForGroupSpecification
{
  [Test]
  public void ShouldAllowGettingAllDirectDevicesFromGroup()
  {
    //GIVEN
    var dev1 = Any.String();
    var dev2 = Any.String();
    var user1 = Any.String();
    var s = new ProductionCode.AuthorizationStructure(Any.Instance<IChangeEventsTarget>());
    s.AddDevice(RootNodeId, dev1);
    s.AddDevice(RootNodeId, dev2);
    s.AddUser(RootNodeId, user1);

    //WHEN
    var deviceIds = s.RetrieveIdsOfDevicesBelongingToGroup(RootNodeId.Name);

    //THEN
    deviceIds.Should().Equal(HashSet.createRange([NodeId.Device(dev1), NodeId.Device(dev2)]));
  }

  [Test] 
  public void ShouldAllowGettingAllDevicesFromGroupsFromTheLevelsBelow()
  {
    //GIVEN
    var dev1 = Any.String();
    var dev2 = Any.String();
    var user1 = Any.String();
    var group1 = Any.String();
    var s = new ProductionCode.AuthorizationStructure(Any.Instance<IChangeEventsTarget>());
    s.AddGroup(RootNodeId, group1);
    s.AddDevice(NodeId.Group(group1), dev1);
    s.AddDevice(NodeId.Group(group1), dev2);
    s.AddUser(RootNodeId, user1);

    //WHEN
    var deviceIds = s.RetrieveIdsOfDevicesBelongingToGroup(RootNodeId.Name);

    //THEN
    deviceIds.Should().Equal(HashSet.createRange([NodeId.Device(dev1), NodeId.Device(dev2)]));
  }

  [Test]
  public void ShouldNotReturnDevicesFromAboveTheGroupWhenAskingForOwnedDevices()
  {
    //GIVEN
    var notOwnedDevice = Any.String();
    var user1 = Any.String();
    var group1 = Any.String();
    var s = new ProductionCode.AuthorizationStructure(Any.Instance<IChangeEventsTarget>());
    s.AddGroup(RootNodeId, group1);
    s.AddDevice(RootNodeId, notOwnedDevice);
    s.AddUser(NodeId.Group(group1), user1);

    //WHEN
    var deviceIds = s.RetrieveIdsOfDevicesBelongingToGroup(group1);

    //THEN
    deviceIds.Should().BeEmpty();
  }

  //BUG: what if id doesn't exist?
  //BUG: what if it's an id of another resource?
}