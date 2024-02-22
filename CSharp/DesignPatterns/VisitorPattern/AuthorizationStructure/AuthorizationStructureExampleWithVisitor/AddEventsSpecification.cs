using AuthorizationStructureExampleWithVisitor.ProductionCode;
using static AuthorizationStructureExampleWithVisitor.ProductionCode.AuthorizationStructure;

namespace AuthorizationStructureExampleWithVisitor;

public class AddEventsSpecification
{
  [Test]
  public void ShouldGenerateDeviceAddedEvent()
  {
    //GIVEN
    var target = Substitute.For<IChangeEventsTarget>();
    var s = new AuthorizationStructure(target);
    var deviceName = Any.String();

    //WHEN
    s.AddDevice(RootNodeId.Name, deviceName, Any.String());

    //THEN
    target.ReceivedOnly(1).Added(NodeId.Device(deviceName), RootNodeId.Just());
  }

  [Test]
  public void ShouldGenerateMultipleDeviceAddedEventsInOrder()
  {
    //GIVEN
    var target = Substitute.For<IChangeEventsTarget>();
    var s = new AuthorizationStructure(target);
    var device1Name = Any.String();
    var device2Name = Any.String();

    //WHEN
    s.AddDevice(RootNodeId.Name, device1Name, Any.String());
    s.AddDevice(RootNodeId.Name, device2Name, Any.String());

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
    var s = new AuthorizationStructure(target);
    var user1 = Any.String();

    //WHEN
    s.AddUser(RootNodeId.Name, user1);

    //THEN
    target.ReceivedOnly(1).Added(NodeId.User(user1), RootNodeId.Just());
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
  public void ShouldGenerateAddedEventsForSubgroupsContainingDevicesAndUsers()
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
    s.AddGroup(RootNodeId.Name, group2Name);
    s.AddGroup(RootNodeId.Name, group3Name);
    s.AddDevice(group1Name, device1Name, Any.String());
    s.AddUser(group2Name, user1Name);
    s.AddUser(group3Name, user2Name);

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