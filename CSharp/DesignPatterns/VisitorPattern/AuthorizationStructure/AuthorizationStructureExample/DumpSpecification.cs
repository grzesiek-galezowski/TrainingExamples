using AuthorizationStructureExample.ProductionCode;
using NSubstitute.ClearExtensions;

namespace AuthorizationStructureExample;

public class DumpSpecification
{
  private NodeId RootId => AuthorizationStructure.RootNodeId;

  [Test]
  public void ShouldDumpSingleDeviceConnectedToRootGroup()
  {
    //GIVEN
    var target = Substitute.For<IChangeEventsTarget>();
    var s = new AuthorizationStructure(target);
    var deviceName = Any.String();

    s.AddDevice(RootId, deviceName);
    target.ClearSubstitute();

    //WHEN
    s.Dump();

    //THEN
    XReceived.Exactly(() =>
    {
      target.Added(RootId, Maybe<NodeId>.Nothing);
      target.Added(NodeId.Device(deviceName), RootId.Just());
    });
  }

  [Test]
  public void ShouldDumpMultipleDevicesConnectedToRootGroup()
  {
    //GIVEN
    var target = Substitute.For<IChangeEventsTarget>();
    var s = new AuthorizationStructure(target);
    var device1Name = Any.String();
    var device2Name = Any.String();

    s.AddDevice(RootId, device1Name);
    s.AddDevice(RootId, device2Name);
    target.ClearSubstitute();

    //WHEN
    s.Dump();

    //THEN
    XReceived.Exactly(() =>
    {
      target.Added(RootId, Maybe<NodeId>.Nothing);
      target.Added(NodeId.Device(device1Name), RootId.Just());
      target.Added(NodeId.Device(device2Name), RootId.Just());
    });
  }

  [Test]
  public void ShouldDumpSingleUserConnectedToRootGroup()
  {
    //GIVEN
    var target = Substitute.For<IChangeEventsTarget>();
    var s = new AuthorizationStructure(target);
    var user1 = Any.String();

    s.AddUser(RootId, user1);
    target.ClearSubstitute();

    //WHEN
    s.Dump();

    //THEN
    XReceived.Exactly(() =>
    {
      target.Added(RootId, Maybe<NodeId>.Nothing);
      target.Added(NodeId.User(user1), RootId.Just());
    });
  }

  [Test]
  public void ShouldDumpSingleGroupConnectedToRootGroup()
  {
    //GIVEN
    var target = Substitute.For<IChangeEventsTarget>();
    var s = new AuthorizationStructure(target);
    var nodeName = Any.String();

    s.AddGroup(RootId, nodeName);
    target.ClearSubstitute();

    //WHEN
    s.Dump();

    //THEN
    XReceived.Exactly(() =>
    {
      target.Added(RootId, Maybe<NodeId>.Nothing);
      target.Added(NodeId.Group(nodeName), RootId.Just());
    });
  }

  [Test]
  public void ShouldDumpMultipleGroupsConnectedToRootGroupWithUsersAndDevices()
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

    s.AddGroup(RootId, group1Name);
    s.AddGroup(RootId, group2Name);
    s.AddGroup(RootId, group3Name);
    s.AddDevice(group1Id, device1Name);
    s.AddUser(group2Id, user1Name);
    s.AddUser(group3Id, user2Name);
    target.ClearSubstitute();

    //WHEN
    s.Dump();

    //THEN
    XReceived.Exactly(() =>
    {
      target.Added(RootId, Maybe<NodeId>.Nothing);
      target.Added(group1Id, RootId.Just());
      target.Added(NodeId.Device(device1Name), group1Id.Just());
      target.Added(group2Id, RootId.Just());
      target.Added(NodeId.User(user1Name), group2Id.Just());
      target.Added(group3Id, RootId.Just());
      target.Added(NodeId.User(user2Name), group3Id.Just());
    });
  }

  [Test]
  public void ShouldDumpMultipleGroupLevels()
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

    s.AddGroup(RootId, group1Name);
    s.AddGroup(group1Id, group2Name);
    s.AddGroup(group2Id, group3Name);
    target.ClearSubstitute();

    //WHEN
    s.Dump();

    //THEN
    XReceived.Exactly(() =>
    {
      target.Added(RootId, Maybe<NodeId>.Nothing);
      target.Added(group1Id, RootId.Just());
      target.Added(group2Id, group1Id.Just());
      target.Added(group3Id, group2Id.Just());
    });
  }

  //BUG: groups with groups

  //BUG: errors, e.g. nonexistent parent, nonexistent id, adding the same group again in the same or different place, adding a child to a device or to a user etc.
  //BUG: filter by network parameters (possible only for devices)
}