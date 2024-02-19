using AuthorizationStructure.ProductionCode;

namespace AuthorizationStructure;

public class DumpSpecification
{
  private NodeId RootId => ProductionCode.AuthorizationStructure.RootNodeId;

  [Test]
  public void ShouldDumpSingleDeviceConnectedToRootGroup()
  {
    //GIVEN
    var s1 = new ProductionCode.AuthorizationStructure();
    var target = Substitute.For<IDumpTarget>();
    var deviceName = Any.String();

    s1.AddDevice(RootId, deviceName);

    //WHEN
    s1.Dump(target);

    //THEN
    XReceived.InOrder(() =>
    {
      target.Add(RootId, Maybe<NodeId>.Nothing);
      target.Add(NodeId.Device(deviceName), RootId.Just());
    }, new FilterAllowingQueries());
  }

  [Test]
  public void ShouldDumpMultipleDevicesConnectedToRootGroup()
  {
    //GIVEN
    var s1 = new ProductionCode.AuthorizationStructure();
    var target = Substitute.For<IDumpTarget>();
    var device1Name = Any.String();
    var device2Name = Any.String();

    s1.AddDevice(RootId, device1Name);
    s1.AddDevice(RootId, device2Name);

    //WHEN
    s1.Dump(target);

    //THEN
    XReceived.InOrder(() =>
    {
      target.Add(RootId, Maybe<NodeId>.Nothing);
      target.Add(NodeId.Device(device1Name), RootId.Just());
      target.Add(NodeId.Device(device2Name), RootId.Just());
    }, new FilterAllowingQueries());
  }

  [Test]
  public void ShouldDumpSingleUserConnectedToRootGroup()
  {
    //GIVEN
    var s1 = new ProductionCode.AuthorizationStructure();
    var target = Substitute.For<IDumpTarget>();
    var user1 = Any.String();

    s1.AddUser(RootId, user1);

    //WHEN
    s1.Dump(target);

    //THEN
    XReceived.InOrder(() =>
    {
      target.Add(RootId, Maybe<NodeId>.Nothing);
      target.Add(NodeId.User(user1), RootId.Just());
    }, new FilterAllowingQueries());
  }

  [Test]
  public void ShouldDumpSingleGroupConnectedToRootGroup()
  {
    //GIVEN
    var s1 = new ProductionCode.AuthorizationStructure();
    var target = Substitute.For<IDumpTarget>();
    var nodeName = Any.String();

    s1.AddGroup(RootId, nodeName);

    //WHEN
    s1.Dump(target);

    //THEN
    XReceived.InOrder(() =>
    {
      target.Add(RootId, Maybe<NodeId>.Nothing);
      target.Add(NodeId.Group(nodeName), RootId.Just());
    }, new FilterAllowingQueries());
  }

  [Test]
  public void ShouldDumpMultipleGroupsConnectedToRootGroupWithUsersAndDevices()
  {
    //GIVEN
    var s1 = new ProductionCode.AuthorizationStructure();
    var target = Substitute.For<IDumpTarget>();
    var group1Name = Any.String();
    var group2Name = Any.String();
    var group3Name = Any.String();
    var device1Name = Any.String();
    var user1Name = Any.String();
    var user2Name = Any.String();
    var group1Id = NodeId.Group(group1Name);
    var group2Id = NodeId.Group(group2Name);
    var group3Id = NodeId.Group(group3Name);

    s1.AddGroup(RootId, group1Name);
    s1.AddGroup(RootId, group2Name);
    s1.AddGroup(RootId, group3Name);
    s1.AddDevice(group1Id, device1Name);
    s1.AddUser(group2Id, user1Name);
    s1.AddUser(group3Id, user2Name);

    //WHEN
    s1.Dump(target);

    //THEN
    XReceived.InOrder(() =>
    {
      target.Add(RootId, Maybe<NodeId>.Nothing);
      target.Add(group1Id, RootId.Just());
      target.Add(NodeId.Device(device1Name), group1Id.Just());
      target.Add(group2Id, RootId.Just());
      target.Add(NodeId.User(user1Name), group2Id.Just());
      target.Add(group3Id, RootId.Just());
      target.Add(NodeId.User(user2Name), group3Id.Just());
    }, new FilterAllowingQueries());
  }

  [Test]
  public void ShouldDumpMultipleGroupLevels()
  {
    //GIVEN
    var s1 = new ProductionCode.AuthorizationStructure();
    var target = Substitute.For<IDumpTarget>();
    var group1Name = Any.String();
    var group2Name = Any.String();
    var group3Name = Any.String();
    var device1Name = Any.String();
    var user1Name = Any.String();
    var user2Name = Any.String();
    var group1Id = NodeId.Group(group1Name);
    var group2Id = NodeId.Group(group2Name);

    s1.AddGroup(RootId, group1Name);
    s1.AddGroup(group1Id, group2Name);
    s1.AddGroup(group2Id, group3Name);

    //WHEN
    s1.Dump(target);

    //THEN
    XReceived.InOrder(() =>
    {
      target.Add(RootId, Maybe<NodeId>.Nothing);
      target.Add(group1Id, RootId.Just());
      target.Add(group2Id, group1Id.Just());
    }, new FilterAllowingQueries());
  }

  //BUG: groups with groups

  //BUG: errors, e.g. nonexistent parent, nonexistent id, adding the same group again in the same or different place, adding a child to a device or to a user etc.
  //BUG: filter by network parameters (possible only for devices)
}