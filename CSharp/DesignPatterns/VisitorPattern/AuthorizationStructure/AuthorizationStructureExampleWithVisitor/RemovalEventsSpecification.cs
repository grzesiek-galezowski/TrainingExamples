using AuthorizationStructureExampleWithVisitor.ProductionCode;
using NSubstitute.ClearExtensions;
using static AuthorizationStructureExampleWithVisitor.ProductionCode.AuthorizationStructure;

namespace AuthorizationStructureExampleWithVisitor;

public class RemovalEventsSpecification
{
  [Test]
  public void ShouldGenerateDeviceRemovedEvent()
  {
    //GIVEN
    var target = Substitute.For<IChangeEventsTarget>();
    var s = new AuthorizationStructure(target);
    var deviceName = Any.String();

    s.AddDevice(RootNodeId.Name, deviceName, Any.String());
    target.ClearReceivedCalls();

    //WHEN
    s.Remove(NodeId.Device(deviceName));

    //THEN
    target.ReceivedOnly(1).Removed(NodeId.Device(deviceName), RootNodeId.Just());
  }

  [Test]
  public void ShouldGenerateMultipleDeviceRemovedEventsInOrder()
  {
    //GIVEN
    var target = Substitute.For<IChangeEventsTarget>();
    var s = new AuthorizationStructure(target);
    var device1Name = Any.String();
    var device2Name = Any.String();

    //WHEN
    s.AddDevice(RootNodeId.Name, device1Name, Any.String());
    s.AddDevice(RootNodeId.Name, device2Name, Any.String());
    target.ClearSubstitute();
    s.Remove(NodeId.Device(device1Name));
    s.Remove(NodeId.Device(device2Name));

    //THEN
    XReceived.Exactly(() =>
    {
      target.Removed(NodeId.Device(device1Name), RootNodeId.Just());
      target.Removed(NodeId.Device(device2Name), RootNodeId.Just());
    });
  }

  [Test]
  public void ShouldGenerateUserRemovedEvent()
  {
    //GIVEN
    var target = Substitute.For<IChangeEventsTarget>();
    var s = new AuthorizationStructure(target);
    var user1 = Any.String();

    //WHEN
    s.AddUser(RootNodeId.Name, user1);
    target.ClearSubstitute();
    s.Remove(NodeId.User(user1));

    //THEN
    target.ReceivedOnly(1).Removed(NodeId.User(user1), RootNodeId.Just());
  }

  [Test]
  public void ShouldGenerateGroupAddedEvent()
  {
    //GIVEN
    var target = Substitute.For<IChangeEventsTarget>();
    var s = new AuthorizationStructure(target);
    var nodeName = Any.String();

    //WHEN
    s.AddGroup(RootNodeId.Name, nodeName);

    //THEN
    target.ReceivedOnly(1).Added(NodeId.Group(nodeName), RootNodeId.Just());
  }

  [Test]
  public void ShouldGenerateRemovedEventsForSubgroupsContainingDevicesAndUsers()
  {
    //GIVEN
    var target = Substitute.For<IChangeEventsTarget>();
    var s = new AuthorizationStructure(target);
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
    s.AddGroup(RootNodeId.Name, group1Name);
    s.AddGroup(group1Name, group2Name);
    s.AddGroup(group2Name, group3Name);
    s.AddDevice(group1Name, device1Name, Any.String());
    s.AddUser(group2Name, user1Name);
    s.AddUser(group3Name, user2Name);
    target.ClearSubstitute();
    s.Remove(NodeId.Group(group1Name));

    //THEN
    XReceived.Exactly(() =>
    {
      target.Removed(group1Id, RootNodeId.Just());
      target.Removed(group2Id, NodeId.Group(group1Name).Just());
      target.Removed(group3Id, NodeId.Group(group2Name).Just());
      target.Removed(NodeId.User(user2Name), group3Id.Just());
      target.Removed(NodeId.User(user1Name), group2Id.Just());
      target.Removed(NodeId.Device(device1Name), group1Id.Just());
    });
  }

  [Test]
  public void ShouldGenerateAddedEventsForNestedGroups()
  {
    //GIVEN
    var target = Substitute.For<IChangeEventsTarget>();
    var s = new AuthorizationStructure(target);
    var group1Name = Any.String();
    var group2Name = Any.String();
    var group3Name = Any.String();
    var group1Id = NodeId.Group(group1Name);
    var group2Id = NodeId.Group(group2Name);
    var group3Id = NodeId.Group(group3Name);

    //WHEN
    s.AddGroup(RootNodeId.Name, group1Name);
    s.AddGroup(group1Name, group2Name);
    s.AddGroup(group2Name, group3Name);

    //THEN
    XReceived.Exactly(() =>
    {
      target.Added(group1Id, RootNodeId.Just());
      target.Added(group2Id, group1Id.Just());
      target.Added(group3Id, group2Id.Just());
    });
  }
}