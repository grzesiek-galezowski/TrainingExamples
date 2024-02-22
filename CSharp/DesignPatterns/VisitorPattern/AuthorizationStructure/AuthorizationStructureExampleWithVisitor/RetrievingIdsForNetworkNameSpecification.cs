using AuthorizationStructureExampleWithVisitor.ProductionCode;
using FluentAssertions;
using LanguageExt;

namespace AuthorizationStructureExampleWithVisitor;

public class RetrievingIdsForNetworkNameSpecification
{
  private NodeId RootId => AuthorizationStructure.RootNodeId;

  [Test]
  public void ShouldAllowGettingAllDevicesNetworkNameFromRoot()
  {
    //GIVEN
    var dev1 = Any.String();
    var dev2 = Any.String();
    var dev3 = Any.String();
    var user1 = Any.String();
    var group1 = Any.String();
    var network1 = Any.String();
    var network2 = Any.String();
    var s = new AuthorizationStructure(Any.Instance<IChangeEventsTarget>());
    s.AddGroup(RootId.Name, group1);
    s.AddDevice(RootId.Name, dev2, network2);
    s.AddUser(RootId.Name, user1);
    s.AddDevice(RootId.Name, dev1, network1);
    s.AddDevice(group1, dev3, network1);

    //WHEN
    var deviceIds = s.RetrieveIdsOfDevicesInNetwork(network1);

    //THEN
    deviceIds.Should().Equal(HashSet.createRange([NodeId.Device(dev1), NodeId.Device(dev3)]));
  }

  [Test] 
  public void ShouldAllowGettingAllDevicesForNetworkNameFromSpecificSubtree()
  {
    //GIVEN
    var dev1 = Any.String();
    var dev2 = Any.String();
    var dev3 = Any.String();
    var user1 = Any.String();
    var group1 = Any.String();
    var network1 = Any.String();
    var network2 = Any.String();
    var s = new AuthorizationStructure(Any.Instance<IChangeEventsTarget>());
    s.AddGroup(RootId.Name, group1);
    s.AddDevice(RootId.Name, dev2, network2);
    s.AddUser(RootId.Name, user1);
    s.AddDevice(RootId.Name, dev1, network1);
    s.AddDevice(group1, dev3, network1);

    //WHEN
    var deviceIds = s.RetrieveIdsOfDevicesInNetworkFromSubtree(group1, network1);

    //THEN
    deviceIds.Should().Equal(HashSet.createRange([NodeId.Device(dev3)]));
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
    s.AddDevice(RootId.Name, notOwnedDevice, Any.String());
    s.AddUser(group1, user1);

    //WHEN
    var deviceIds = s.RetrieveIdsOfDevicesOwnedByUser(user1);

    //THEN
    deviceIds.Should().BeEmpty();
  }
}