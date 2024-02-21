using AuthorizationStructureExample.ProductionCode;
using FluentAssertions;
using LanguageExt;
using static AuthorizationStructureExample.ProductionCode.AuthorizationStructure;

namespace AuthorizationStructureExample;

public class RetrievingIdsForGroupSpecification
{
  [Test]
  public void ShouldAllowGettingAllDirectDevicesFromGroup()
  {
    //GIVEN
    var dev1 = Any.String();
    var dev2 = Any.String();
    var user1 = Any.String();
    var s = new AuthorizationStructure(Any.Instance<IChangeEventsTarget>());
    s.AddDevice(RootNodeId.Name, dev1, Any.String());
    s.AddDevice(RootNodeId.Name, dev2, Any.String());
    s.AddUser(RootNodeId.Name, user1);

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
    var s = new AuthorizationStructure(Any.Instance<IChangeEventsTarget>());
    s.AddGroup(RootNodeId.Name, group1);
    s.AddDevice(group1, dev1, Any.String());
    s.AddDevice(group1, dev2, Any.String());
    s.AddUser(RootNodeId.Name, user1);

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
    var s = new AuthorizationStructure(Any.Instance<IChangeEventsTarget>());
    s.AddGroup(RootNodeId.Name, group1);
    s.AddDevice(RootNodeId.Name, notOwnedDevice, Any.String());
    s.AddUser(group1, user1);

    //WHEN
    var deviceIds = s.RetrieveIdsOfDevicesBelongingToGroup(group1);

    //THEN
    deviceIds.Should().BeEmpty();
  }

  //BUG: what if id doesn't exist?
}