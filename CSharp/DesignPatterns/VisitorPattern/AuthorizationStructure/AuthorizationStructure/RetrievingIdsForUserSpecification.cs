using AuthorizationStructure.ProductionCode;
using FluentAssertions;
using LanguageExt;

namespace AuthorizationStructure;

public class RetrievingIdsForUserSpecification
{
  private NodeId RootId => ProductionCode.AuthorizationStructure.RootNodeId;

  [Test]
  public void ShouldAllowGettingAllDevicesForUserFromTheSameLevel()
  {
    //GIVEN
    var dev1 = Any.String();
    var dev2 = Any.String();
    var user1 = Any.String();
    var s = new ProductionCode.AuthorizationStructure(Any.Instance<IChangeEventTarget>());
    s.AddDevice(RootId, dev1);
    s.AddDevice(RootId, dev2);
    s.AddUser(RootId, user1);

    //WHEN
    var deviceIds = s.RetrieveIdsOfDevicesOwnedByUser(user1);

    //THEN
    deviceIds.Should().Equal(HashSet.createRange([NodeId.Device(dev1), NodeId.Device(dev2)]));
  }

  [Test] 
  public void ShouldAllowGettingAllDevicesForUserFromTheLevelsBelow()
  {
    //GIVEN
    var dev1 = Any.String();
    var dev2 = Any.String();
    var user1 = Any.String();
    var group1 = Any.String();
    var s = new ProductionCode.AuthorizationStructure(Any.Instance<IChangeEventTarget>());
    s.AddGroup(RootId, group1);
    s.AddDevice(NodeId.Group(group1), dev1);
    s.AddDevice(NodeId.Group(group1), dev2);
    s.AddUser(RootId, user1);

    //WHEN
    var deviceIds = s.RetrieveIdsOfDevicesOwnedByUser(user1);

    //THEN
    deviceIds.Should().Equal(HashSet.createRange([NodeId.Device(dev1), NodeId.Device(dev2)]));
  }

  [Test]
  public void ShouldNotReturnDevicesFromAboveDirectUserGroupWhenAskingForOwnedDevices()
  {
    //GIVEN
    var notOwnedDevice = Any.String();
    var user1 = Any.String();
    var group1 = Any.String();
    var s = new ProductionCode.AuthorizationStructure(Any.Instance<IChangeEventTarget>());
    s.AddGroup(RootId, group1);
    s.AddDevice(RootId, notOwnedDevice);
    s.AddUser(NodeId.Group(group1), user1);

    //WHEN
    var deviceIds = s.RetrieveIdsOfDevicesOwnedByUser(user1);

    //THEN
    deviceIds.Should().BeEmpty();
  }

  //BUG: what if id doesn't exist?
  //BUG: what if it's an id of another resource?
}