using AuthorizationStructureExample.ProductionCode;
using FluentAssertions;
using LanguageExt;

namespace AuthorizationStructureExample;

public class RetrievingIdsForUserSpecification
{
  private NodeId RootId => AuthorizationStructure.RootNodeId;

  [Test]
  public void ShouldAllowGettingAllDevicesForUserFromTheSameLevel()
  {
    //GIVEN
    var dev1 = Any.String();
    var dev2 = Any.String();
    var user1 = Any.String();
    var s = new AuthorizationStructure(Any.Instance<IChangeEventsTarget>());
    s.AddDevice(RootId.Name, dev1);
    s.AddDevice(RootId.Name, dev2);
    s.AddUser(RootId.Name, user1);

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
    var s = new AuthorizationStructure(Any.Instance<IChangeEventsTarget>());
    s.AddGroup(RootId.Name, group1);
    s.AddDevice(group1, dev1);
    s.AddDevice(group1, dev2);
    s.AddUser(RootId.Name, user1);

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
    var s = new AuthorizationStructure(Any.Instance<IChangeEventsTarget>());
    s.AddGroup(RootId.Name, group1);
    s.AddDevice(RootId.Name, notOwnedDevice);
    s.AddUser(group1, user1);

    //WHEN
    var deviceIds = s.RetrieveIdsOfDevicesOwnedByUser(user1);

    //THEN
    deviceIds.Should().BeEmpty();
  }

  //BUG: what if id doesn't exist?
}