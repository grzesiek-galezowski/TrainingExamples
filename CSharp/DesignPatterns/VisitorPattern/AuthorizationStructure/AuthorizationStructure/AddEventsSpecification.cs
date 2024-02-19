using AuthorizationStructure.ProductionCode;
using static AuthorizationStructure.ProductionCode.AuthorizationStructure;

namespace AuthorizationStructure;

public class AddEventsSpecification
{
  [Test]
  public void ShouldGenerateDeviceAddedEvent()
  {
    //GIVEN
    var target = Substitute.For<IChangeEventsTarget>();
    var s = new ProductionCode.AuthorizationStructure(target);
    var deviceName = Any.String();

    //WHEN
    s.AddDevice(RootNodeId, deviceName);

    //THEN
    target.ReceivedOnly(1).Added(NodeId.Device(deviceName), RootNodeId.Just());
  }

  [Test]
  public void ShouldGenerateMultipleDeviceAddedEventsInOrder()
  {
    //GIVEN
    var target = Substitute.For<IChangeEventsTarget>();
    var s = new ProductionCode.AuthorizationStructure(target);
    var device1Name = Any.String();
    var device2Name = Any.String();

    //WHEN
    s.AddDevice(RootNodeId, device1Name);
    s.AddDevice(RootNodeId, device2Name);

    //THEN
    XReceived.Exactly(() =>
    {
      target.Added(NodeId.Device(device1Name), RootNodeId.Just());
      target.Added(NodeId.Device(device2Name), RootNodeId.Just());
    });
  }

  [Test]
  public void ShouldGenerateUserAddedEvent()
  {
    //GIVEN
    var target = Substitute.For<IChangeEventsTarget>();
    var s = new ProductionCode.AuthorizationStructure(target);
    var user1 = Any.String();

    //WHEN
    s.AddUser(RootNodeId, user1);

    //THEN
    target.ReceivedOnly(1).Added(NodeId.User(user1), RootNodeId.Just());
  }

  [Test]
  public void ShouldGenerateGroupAddedEvent()
  {
    //GIVEN
    var target = Substitute.For<IChangeEventsTarget>();
    var s = new ProductionCode.AuthorizationStructure(target);
    var nodeName = Any.String();

    //WHEN
    s.AddGroup(RootNodeId, nodeName);

    //THEN
    target.ReceivedOnly(1).Added(NodeId.Group(nodeName), RootNodeId.Just());
  }

  [Test]
  public void ShouldGenerateAddedEventsForSubgroupsContainingDevicesAndUsers()
  {
    //GIVEN
    var target = Substitute.For<IChangeEventsTarget>();
    var s = new ProductionCode.AuthorizationStructure(target);
    var group1Name = Any.String();
    var group2Name = Any.String();
    var group3Name = Any.String();
    var device1Name = Any.String();
    var user1Name = Any.String();
    var user2Name = Any.String();
    var group1Id = NodeId.Group(group1Name);
    var group2Id = NodeId.Group(group2Name);
    var group3Id = NodeId.Group(group3Name);

    //WHEN
    s.AddGroup(RootNodeId, group1Name);
    s.AddGroup(RootNodeId, group2Name);
    s.AddGroup(RootNodeId, group3Name);
    s.AddDevice(group1Id, device1Name);
    s.AddUser(group2Id, user1Name);
    s.AddUser(group3Id, user2Name);

    //THEN
    XReceived.Exactly(() =>
    {
      target.Added(group1Id, RootNodeId.Just());
      target.Added(group2Id, RootNodeId.Just());
      target.Added(group3Id, RootNodeId.Just());
      target.Added(NodeId.Device(device1Name), group1Id.Just());
      target.Added(NodeId.User(user1Name), group2Id.Just());
      target.Added(NodeId.User(user2Name), group3Id.Just());
    });
  }

  [Test]
  public void ShouldGenerateAddedEventsForNestedGroups()
  {
    //GIVEN
    var target = Substitute.For<IChangeEventsTarget>();
    var s = new ProductionCode.AuthorizationStructure(target);
    var group1Name = Any.String();
    var group2Name = Any.String();
    var group3Name = Any.String();
    var group1Id = NodeId.Group(group1Name);
    var group2Id = NodeId.Group(group2Name);
    var group3Id = NodeId.Group(group3Name);

    //WHEN
    s.AddGroup(RootNodeId, group1Name);
    s.AddGroup(group1Id, group2Name);
    s.AddGroup(group2Id, group3Name);

    //THEN
    XReceived.Exactly(() =>
    {
      target.Added(group1Id, RootNodeId.Just());
      target.Added(group2Id, group1Id.Just());
      target.Added(group3Id, group2Id.Just());
    });
  }

  //BUG: errors, e.g. nonexistent parent, nonexistent id, adding the same group again in the same or different place, adding a child to a device or to a user etc.
  //BUG: filter by network parameters (possible only for devices)
}